# Bitcoin-RPC-Rebroadcaster
A simple console app, written in c#, that can re-broadcast Bitcoin transactions using your own node.


# Please only use this program on your local network where your Bitcoin node is hosted! 
Add this to your bitcoin.conf file in order to use this program:
```server=1
rpcallowip=127.0.0.1
rpcallowip=Local IP of another machine that hosts a Bitcoin node. This is optional.
rpcbind=127.0.0.1
listen=1
rpcuser=StrongUserName
rpcpassword=StrongPassword
rpcport=8332
```
If you plan to experiment with free (Zero fee) transactions, also add this to your .conf file:
```
minrelaytxfee=0
```
Wait until your node is fully synced before trying to use this software.

Pro-Tip: If you have already broadcasted your transaction to the network (For example you sent it from your wallet) and you wish to keep re-broadcasting it, but don't have the raw hex transaction, there is a simple way to get it. 
1) Open one of the following websites:
http://bitchain.tk
https://btc.chaintools.io

2) Search for your transaction using its TXID. (It looks something like this: e01ca67d73ffb028540b32b8b71345fd575b673813bb4807295dc1bb0033c829)

3) Click on JSON

4) Scroll all the way down where you'll find a `"hex":` field.

5) Copy everything from it except the quotation marks --> This is your raw transaction in hex format.

Alternatively, you can also use your own node for this task. Open the RPC console in the debug window and enter `getrawtransaction yourTXID` 
Bitcoin Core will return your transaction in hex format.
