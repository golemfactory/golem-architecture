# Specification for deposits functionality.

### Motivation

We want solution for the following problem:

1. User A wants to use the Golem Network to perform a computation, but don't want to run yagna.
2. User A wants person B to run yagna for them.
3. User A wants to give allowance for spending his funds to person B.
4. Person B wants to be sure that User A will pay for the computation.
5. Person B wants to be paid for being middle man (for example cover gas costs and/or earn some money)

Funder (User A) - Person that is funding the deposit and requesting job from the Spender service

![funder](deposit_assets/funder300.webp)

Spender (Person B) - Service based on yagna

![spender](deposit_assets/spender300.webp)


### Solution attempt allowance

The simple solution is to use ERC20 allowance to person B funds, but this solution has some drawbacks:
Point 4 is not satisfied, because User A can revoke allowance at any time.
Point 5 is not satisfied, because Person B can't be sure that User A will pay for the computation.

### Solution 

Contract that will hold funds until specified time.

![lock](deposit_assets/lock300.webp)

We are using following interface:

``` solidity
struct Deposit {
    address spender; //address that can spend the funds provided by customer
    uint128 amount; //remaining funds locked
    uint128 feeAmount; //fee amount locked for spender
    uint64 validTo; //after this timestamp funds can be returned to customer
}

struct DepositView {
    uint256 id;     //unique id
    uint64 nonce;  //nonce unique for each funder
    address funder; //address that can spend the funds provided by customer
    address spender; //address that can spend the funds provided by customer
    uint128 amount; //remaining funds locked
    uint128 feeAmount; //fee amount locked for spender
    uint64 validTo; //after this timestamp funds can be returned to customer
}
interface ILockPayment {
    // createDeposit - Customer locks funds for usage by spender
    //
    // id - unique id (build from Funder address and nonce)
    // spender - the address that is allowed to spend the funds regardless of time
    // amount - amount of GLM tokens to lock
    // flatFeeAmount - amount of GLM tokens given to spender (non-refundable). Fee is claimed by spender when called payoutSingle or payoutMultiple first time.
    // percentFee - percent fee as percent of amount (given in parts/per million), so 1000 gives 0.1 %.
    //              if given negative it is deducted from flatFeeAmount
    // blockNo - block number until which funds are guaranteed to be locked for spender.
    //           Spender still can use the funds after this block,
    //           but customer can request the funds to be returned clearing deposit after validToTimestamp.
    function createDeposit(uint64 nonce, address spender, uint128 amount, uint128 flatFeeAmount, int64 percentFee, uint64 validToTimestamp) external returns (uint256);
    function extendDeposit(uint64 nonce, uint128 additionalAmount, uint128 additionalFlatFee, uint64 validToTimestamp) external;
    // Spender can close deposit anytime claiming fee and returning rest of funds to Funder
    function closeDeposit(uint256 id) external;
    // Funder can terminate deposit after validTo date elapses
    function terminateDeposit(uint64 nonce) external;
    function depositSingleTransfer(uint256 id, address addr, uint128 amount) external;
    function depositTransfer(uint256 id, bytes32[] calldata payments) external;
    function depositSingleTransferAndClose(uint256 id, address addr, uint128 amount) external;
    function depositTransferAndClose(uint256 id, bytes32[] calldata payments) external;
    function getMyDeposit(uint64 nonce) external view returns (DepositView memory);
    function getDeposit(uint256 id) external view returns (DepositView memory);
    function getDepositByNonce(uint64 nonce, address funder) external view returns (DepositView memory);
}

```

Additional events emitted by the contract (for easier web3 integration). There is no need for
extra options emitted by the contract, because additional information can be extracted using getDeposit(id) function.

``` solidity
    event DepositCreated(uint256 id, address spender);
    event DepositExtended(uint256 id, address spender);
    event DepositClosed(uint256 id, address spender);
    event DepositTerminated(uint256 id, address spender);
```

Deposits are stored in the contract using deposit ID:
```solidity
    // deposit is stored using arbitrary id
    mapping(uint256 => Deposit) public deposits;
```

Deposit ID consists of funder address and nonce merged together into U256 id:
Nonce is chosen by funder when creating deposit.

```solidity
    function idFromNonce(uint64 nonce) public view returns (uint256) {
        return idFromNonceAndFunder(nonce, msg.sender);
    }
    function idFromNonceAndFunder(uint64 nonce, address funder) public pure returns (uint256) {
        return (uint256(uint160(funder)) << 96) ^ uint256(nonce);
    }
    function nonceFromId(uint256 id) public pure returns (uint64) {
        return uint64(id);
    }
    function funderFromId(uint256 id) public pure returns (address) {
        return address(uint160(id >> 96));
    }
```

### Yagna implementation

Deposit
```rust
#[derive(Clone, Debug, PartialEq, Serialize, Deserialize)]
pub struct Deposit {
    pub id: String,
    pub contract: String,
}
```
Deposit view (read from contract given using ```getDeposit(uint256 id)```)
```rust
#[derive(Clone, Debug, PartialEq, Serialize, Deserialize)]
pub struct DepositView {
    pub id: String,
    pub nonce: u64,
    pub funder: String,
    pub spender: String,
    pub amount: u128,
    pub fee_amount: u128,
    pub valid_to: u64,
}
```

Api extension of optional field deposit in ya-client
```yaml
    Allocation:
      properties:
...
        deposit:
          type: object
          properties:
            id:
              type: string
            contract:
              type: string
          required:
            - id
            - contract
```

Changes in erc20_payment_lib and yagna has to be made to enable payments directly from deposit
using methods.
```
deposit_single_transfer(id, ...
deposit_transfer(id, ...
deposit_single_transfer_and_close(id, ...
deposit_transfer_and_close(id, ...
```

### Flow

![flow](deposit_assets/flow300.webp)

1. Funder creates deposit using createDeposit function getting deposit ID.
2. Funder sends deposit ID to Spender.
3. Spender uses deposit ID to create allocation on yagna.
4. Spender start processing tasks (agreemets with Providers)
5. Funder can extend deposit using extendDeposit function.
6. Spender can ammend allocation using deposit ID.
7. Spender when finished can close deposit using closeDeposit function.
8. Alternatevily if Spender fail to close allocation Funder can terminate deposit using terminateDeposit function after validTo date elapses.


### Example usage flow chart

```mermaid
sequenceDiagram
    actor Funder
    actor Spender
    participant Web3 as Web3 (Polygon/Holesky)
    participant yagna as yagna (Golem Node)
    participant providers as Providers (ie. VM)
    Funder->>Spender: Greet and request work
    Spender->>Funder: Propose createDeposit
    Funder->>Web3: Send createDeposit
    Web3->>Spender: Confirm createDeposit
    Spender->>yagna: Create allocation from deposit id
    yagna->>Web3: Check if deposit valid
    Funder->>Spender: Ask for work
    Spender->>yagna: Send work
    yagna->>providers: proxy work
    yagna->>providers: pay via deposit contract
    Funder->>Spender: More work
    Spender->>Funder: Propose extendDeposit
    Funder->>Web3: Send extendDeposit
    Web3->>Spender: Confirm extendDeposit
    Spender->>yagna: Ammend allocation with extendedDeposit
    Spender->>yagna: Send more work
    yagna->>providers: Proxy more work
    yagna->>providers: Pay providers with deposit contracts
    Funder->>Spender: Thats enough work
    Spender->>yagna: Finish and close allocation
    yagna->>providers: Pay all pending invoices/debit notes
    yagna->>Web3: Call close allocation
    Web3->>Spender: Pay fee
    Web3->>Funder: Returns remaining funds
```

### Chart explaining flow of GLM tokens 

```mermaid
flowchart TD
    Funder(Funder wallet) -->|GLM transfer| Contract{Contract with locked GLM}
    Contract -->|From Fee part| E[Spender]
    Contract -->|From Main deposit| Provider1[Provider1]
    Contract -->|From Main deposit| Provider2[Provider2]
    Contract -->|Close or terminate| Funder[Funder wallet]
```

## Fee claim

![greedy](deposit_assets/greedy300.webp)

Fee is claimed when deposit is close by spender. Fee amount depends on implementation of the contract implementation.
For example it can be flat fee, percent fee or combination of both.

Fee schema is completely up to the contract and service implementation. Yagna doesn't care really,
we should probably provide some API exposing gas costs so the service can calculate fee based on that.

## Additional safety for Funder

![padlocks](deposit_assets/padlocks300.webp)

* If Spender fail to close allocation due some kind of service failure, 
Funder can terminate deposit after validTo date elapses taking back remaining funds and fee.
* Spender can do whatever he wants with the locked funds, so the Funder has to trust the Spender.
* Spender doesn't have to trust the Funder, which is most important part of the solution.

## Benefits for providers

![featurebox](deposit_assets/featurebox300.webp)

Unfortunately we don't know yet how providers can benefit from this feature.
They are not notified about deposit ID, because we don't see how can it benefit them.
We are open for suggestions, but this solution was designed to solve other problem.

To benefit providers we need to implement something like locking funds for every provider separatly.

Who cares about poor providers anyway?

![provider](deposit_assets/provider300.webp)

