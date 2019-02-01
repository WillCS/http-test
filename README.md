# HTTP Test
I wanted to play around with http requests, so I built a little web server using express and a client program using .NET.

This also serves as my way of learning how to set up a node Typescript environment and how to develop with C# outside of Visual Studio.

You can interact with the server through the client by teaching it words and checking what words it knows.
```
add <words...>
```
Use the `add` command to teach the server some new words. It'll let you know which words it's learning for the first time and which words it already knows.

```
get
```
Use the `get` command to see which words the server knows.