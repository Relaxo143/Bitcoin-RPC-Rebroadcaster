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
