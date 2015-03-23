-- Getting started --

This program use Entity Framework Code First to store transaction data and blockchain status.
Before running for first time, open Package Manager Console and run: Update-Database to create the database.
Also update the NxtUri and SecretPhrase in Program.cs.

-- Architecture --


           ------------------ 
           ExchangeController
           ------------------ 
             |       |
     --------|       |--------
     |                       |
------------          --------------
NxtConnector          NxtRepository
------------          --------------
     |                    |
	 |                    |
--------------        ---------
The Nxt Server        Database
--------------        ---------



ExchangeController is the main entrypoint, that will, when started, fire events when a new inbound transaction is found (see event IncomingTransaction), or 
when an existing transaction gets a new status (see event UpdatedTransactionStatus).
It will use a NxtRepository class to store transaction and blockchain status, however this is only for the internal use of the ExchangeController.
NxtConnector is responsible for communicating with the NXT server in a way that is understood by the ExchangeController.


-- Glossary --

Confirmation levels (aka transaction status):
0-9    confirmations: Pending
10-719 confirmations: Confirmed*
720+   confirmations: Secured

* A transaction is only confirmed if it does not in risk of getting orphaned in a block reorganisation.
See TransactionStatusCalculator or page 6 here: http://nxtinside.org/downloads/nxt-integration.pdf

-- Dependencies --

* EntityFramework
* NxtLib (open source)

-- Todo --

* Withdraw functionality
* Error handling