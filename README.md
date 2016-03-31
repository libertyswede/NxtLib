# .NET Framework API for the Nxt platform

NxtLib is a .NET library that wraps around Json API in the Nxt platform (http://nxt.org/).
NxtLib simplifies the development process when integrating to Nxt as it translates all json data into typed .NET code.<br />
Target framework versions: .NETFramework4.0, .NETFramework4.5 and .NETPlatform5.0

It currently supports NXT version 1.8.0e 

### Getting started
There are a couple of [example programs](https://github.com/libertyswede/NxtLib/tree/master/Examples) that you can check out.<br />
The first example is checking the account balance and also sending some Nxt, you can check out the [code here](https://github.com/libertyswede/NxtLib/blob/master/Examples/NxtConsoleDemo/Program.cs), or the [youtube video](https://www.youtube.com/watch?v=jc8BqEKIRjg) where the code is explained in detail.<br />
<br />
A bit more advanced is the local signing feature, where you sign the transaction within your .NET application. It's useful if you don't have any NXT node that can be trusted. <br />
There's a sample code of how to use that [here](https://github.com/libertyswede/NxtLib/blob/master/Examples/LocalSignedAssetPurchase/Program.cs), and a [youtube video](https://www.youtube.com/watch?v=_H_xbLSSGkY) explaining the functionality.

### Support or contact
There's a [thread about NxtLib](https://nxtforum.org/api-discussion/nxtlib-a-typed-net-api-wrapper-for-nxt/) on the nxtforum.org. You're welcome to join!
