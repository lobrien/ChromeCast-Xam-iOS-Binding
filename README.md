ChromeCast-Xam-iOS-Binding
==========================

Allows the ChromeCast to be programmed using Xamarin.iOS

# Incomplete!
In order to use this, you will need to download Google's ChromeCast SDK from their [developer's site](https://developers.google.com/cast/). 

- Extract the framework
- In Xamarin Studio, create an "iOS Binding Project" 
- Add the framework
- Add `ApiDefinition.cs` 
- Compile it into a .dll, which you can reference from your applications

# Preliminary!
The ChromeCast SDK is in beta as of this writing (2013-08-11) and seems certain to evolve. When that happens, the binding will  need to be updated.

Because the SDK is almost certainly going to change, I haven't made any attempts to "C#-ify" the binding. I haven't normalized properties, created events, etc. beyond what was strictly necessary.

I used [Objective Sharpie](http://docs.xamarin.com/guides/ios/advanced_topics/binding_objective-c/objective_sharpie) to generate 90% of the binding. 

This is my first binding project, so I may have made egregious errors. Be kind and send pull requests.

