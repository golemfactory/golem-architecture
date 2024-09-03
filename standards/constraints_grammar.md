# Demand & Offer Specification Language - Constraints Grammar

## Demand & Offer Constraint Definition

The string representation of an LDAP search filter is a string of
UTF-8 [RFC3629] encoded Unicode characters [Unicode] that is defined
by the following grammar, following the ABNF notation defined [here](https://www.rfc-editor.org/rfc/rfc822). 
The filter format uses a prefix notation.

```
      filter         = LPAREN filtercomp RPAREN
      filtercomp     = and / or / not / item
      and            = AMPERSAND filterlist
      or             = VERTBAR filterlist
      not            = EXCLAMATION filter
      filterlist     = 1*filter
      item           = simple / present / substring / extensible
      simple         = attr filtertype assertionvalue
      filtertype     = equal / approx / greaterorequal / lessorequal
      equal          = EQUALS
      greater        = RANGLE
      less           = LANGLE
      greaterorequal = RANGLE EQUALS
      lessorequal    = LANGLE EQUALS
      present        = attr EQUALS ASTERISK
      substring      = attr EQUALS [initial] any [final]
      initial        = assertionvalue
      any            = ASTERISK *(assertionvalue ASTERISK)
      final          = assertionvalue
      assertionvalue = valueencoding
      attr           = attribute / typedattribute 
      attribute      = attributedescription / attrwithaspect
      attrwithaspect = attributedescription LBRACKET aspect RBRACKET
      aspect         = attributedescription
      typedattribute = attribute DOLLAR typecharacter
      typecharacter  = "v" / "d" / "t"
      valueencoding  = 0*(normal / escaped)
      normal         = UTF1SUBSET
      escaped        = ESC HEX HEX
      UTF1SUBSET     = <see below>
      EXCLAMATION    = "!"
      AMPERSAND      = "&"
      ASTERISK       = "*"
      VERTBAR        = "|"
      EQUALS         = "="
      RANGLE         = ">"
      LANGLE         = "<"
      RBRACKET       = "]"
      LBRACKET       = "["
      ESC            = "\"
      DOLLAR         = "$"
      HEX            = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7" / "8" / "9" / "a" / "A" / "b" / "B" / "c" / "C" / "d" / "D" / "e" / "E" / "f" / "F"

```

### `attributedescription`

The `attributedescription` rule specifies the allowed attribute names. 
An attribute name may consist of any Unicode character apart from the following:

`(`, `)`, `[`, `]`, `=`, `<`, `>`, `\`, `$`

### `valueencoding`

The `valueencoding` rule ensures that the entire filter string is a
valid UTF-8 string and provides that the octets that represent the
ASCII characters `*` (ASCII 0x2a), `(` (ASCII 0x28), `)` (ASCII
0x29), `\` (ASCII 0x5c), and `NUL` (ASCII 0x00) are represented as a
backslash `\` (ASCII 0x5c) followed by the two hexadecimal digits
representing the value of the encoded octet.

This simple escaping mechanism eliminates filter-parsing ambiguities
and allows any filter that can be represented in LDAP to be
represented as a NUL-terminated string.  Other octets that are part
of the `normal` set may be escaped using this mechanism, for example,
non-printing ASCII characters.

For AssertionValues that contain UTF-8 character data, each octet of
the character to be escaped is replaced by a backslash and two hex
digits, which form a single octet in the code of the character.  For
example, the filter checking whether the `cn` attribute contained a
value with the character `*` anywhere in it would be represented as
`(cn=*\2a*)`.


### AttributeDescription

An `AttributeDescription` is a string of characters which may include any characters apart of:

    Character
    ------------------------------
    =
    <
    >
    (
    )
    [
    ]
    $
    \


### Whitespaces

Any whitespace characters separating constraint expression `filter` elements specified in the grammar above - shall be ignored. This implies, that constraint expression can be freely formatted (eg. indentation, etc), as long as the formatting whitespace is added outside of individual filter expressions (delimited with `()`).

## Examples

This section gives a few examples of search filters written using
this notation.

    (cn=Babs Jensen)
    (!(cn=Tim Howes))
    (&(objectClass=Person)(|(sn=Jensen)(cn=Babs J*)))
    (o=univ*of*mich*)

The following examples illustrate the use of the escaping mechanism.

    (o=Parens R Us \28for all your parenthetical needs\29)
    (cn=*\2A*)
    (filename=C:\5cMyFile)
    (bin=\00\00\00\04)
    (sn=Lu\c4\8di\c4\87)

The first example shows the use of the escaping mechanism to
represent parenthesis characters. The second shows how to represent a
"*" in a value, preventing it from being interpreted as a substring
indicator. The third illustrates the escaping of the backslash
character.

The fourth example shows a filter searching for the four-byte value
0x00000004, illustrating the use of the escaping mechanism to
represent arbitrary data, including NUL characters.

The final example illustrates the use of the escaping mechanism to
represent various non-ASCII UTF-8 characters.
