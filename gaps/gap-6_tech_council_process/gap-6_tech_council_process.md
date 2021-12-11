---
gap: 6
title: Tech Council process
description: Rules of operation for Golem Tech Council
author: stranger80 (@stranger80)
status: Draft
type: Meta
---

## Abstract
This is proposal to structure the proceedings of Golem Tech Council in order to streamline the flow of agenda items and make the approval process efficient.

## Motivation
The purpose is to ensure the Tech Council operates efficiently, with as few synchronous actions (like meetings) as possible, and with transparency to all Golem Factory team members.

## Specification

The main purpose of this proposed process is to make proceedings of Tech Council items more efficient and asynchronous (eg. have them raised and reviewed offline, and only use Tech Council sessions to discuss questions on items which have been already digested by participants). 

This is why the responsibilities of the Facilitator also must include upfront facilitation of the Tech Council session agenda so that the participants are given an early heads-up and are able to prepare for the session (eg. by reading and reviewing items proposed on the agenda).

This section specifies the rules for Tech Council proceedings, by defining key elements and workflow.

### Tech Council Members

Two roles of Tech Council members are specified:
- **Participant** - participant of Tech Council proceedings and sessions
- **Facilitator** - responsible for:
  - reviewing and managing the Kanban board and GAP board
  - preparing agenda for Tech Council sessions
  - facilitating Tech Council sessions

### Tech Council Kanban

A Notion board where all Tech Council items are recorded and their status indicated.

### Raising items for Tech Council

The topics for discussion by Tech Council can be raised by any Tech Council member, in a form of:
- **A card on Tech Council Kanban** 
  
  **NOTE**: the card raised on Kanban should include following elements at minimum:
  - Indicative title
  - Description sufficient to set the context and topic of the item in detail allowing for readers to understand and build opinion on the matter.
  - Indication of expected input from Tech Council (ie. **what is Tech Council expected to do with this item?**)

- **A GAP item**

  Just raise a GAP as per [GAP Process](../gap-1_gap_process/gap-1_gap_process.md)

The newly raised topics shall be reviewed by the **Facilitator** and included in the agenda for Tech Council session (wth comments if relevant).

### Tech Council Session

A fortnightly (once per 2 weeks) Tech Council session is an opportunity for participants to address the questions which have been raised against the Tech Council items. 

- A Tech Council session **agenda** is prepared by the **Facilitator** and announced to all participants **at least 2 days prior** to the scheduled session, via email & #tech-council channel for all interested parties to be able to join.
- The **agenda** lists items with respective **authors**, requesting **authors** to be present for the session.
- The session should cover the items listed in the proposed **agenda**, aiming to:
  - approve the item (by pushing it to next stage of its workflow as per its Kanban)
  - respond to the **author** of the item requesting for eg. more details, or response to specific question
  - reject the item
  - setup an offline working group to provide more details for the item
  - other, unusual actions.   

## Rationale
This proposal is a response to Tech Council approach whereby all Tech Council members were meeting weekly and viewing/reading/explaining the status of all items on Kanban board. This approach was inefficient.

## Backwards Compatibility
There is no backward compatibility issue - we are proposing to improve the process and leave no dependencies on its previous shape.  

## Test Cases
N/A

## Security Considerations
The proceedings of Tech Council are to happen using online conferencing, Notion boards, GAPs and documents which all comply with Golem Factory's information security policies. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).