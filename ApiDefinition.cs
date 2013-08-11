using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ChromeCast {
    public enum GCKPlayerState {
        Unknown = -1,
        Idle = 0,
        Stopped = 1,
        Playing = 2
    }

    public enum GCKMediaTrackType {
        Unknown = 0,
        Subtitles = 1,
        Captions = 2,
        Audio = 3,
        Video = 4
    }




    [BaseType (typeof (NSObject))]
    public partial interface GCKApplicationChannel {

        [Export ("delegate")]
        GCKApplicationChannelDelegate Delegate { get; set; }

        [Export ("sendBufferAvailableBytes")]
        int SendBufferAvailableBytes { get; }

        [Export ("sendBufferPendingBytes")]
        int SendBufferPendingBytes { get; }

        [Export ("initWithBufferSize:pingInterval:")]
        IntPtr Constructor (int bufferSize, double pingInterval);

        [Export ("connectTo:")]
        bool ConnectTo (NSUrl url);

        [Export ("disconnect")]
        void Disconnect ();

        [Export ("attachMessageStream:")]
        bool AttachMessageStream (GCKMessageStream stream);

        [Export ("detachMessageStream:")]
        bool DetachMessageStream (GCKMessageStream stream);

        [Export ("detachAllMessageStreams")]
        void DetachAllMessageStreams ();
    }

    [Category, BaseType (typeof (GCKApplicationChannel))]
    public partial interface UnitTestSupport_GCKApplicationChannel {

        [Export ("dispatchMessage:")]
        void DispatchMessage (string message);

        [Export ("sendMessageWithNamespace:message:")]
        bool SendMessageWithNamespace (string nameSpace, NSObject message);
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKApplicationChannelDelegate {

        [Export ("applicationChannelDidConnect:")]
        void  DidConnect(GCKApplicationChannel channel);

        [Export ("applicationChannel:connectionDidFailWithError:")]
        void ConnectionDidFailWithError (GCKApplicationChannel channel, GCKError error);

        [Export ("applicationChannel:didDisconnectWithError:")]
        void DidDisconnectWithError (GCKApplicationChannel channel, GCKError error);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKApplicationMetadata {

        [Export ("name")]
        string Name { get; }

        [Export ("title")]
        string Title { get; }

        [Export ("publisher")]
        string Publisher { get; }

        [Export ("version")]
        string Version { get; }

        [Export ("secondScreenLaunchInfo")]
        string SecondScreenLaunchInfo { get; }

        [Export ("iconURL")]
        NSUrl IconURL { get; }

        [Export ("initWithName:title:publisher:version:secondScreenLaunchInfo:iconURL:supportedProtocols:")]
        IntPtr Constructor (string name, string title, string publisher, string version, string launchInfo, NSUrl iconURL, NSObject [] supportedProtocols);

        [Export ("doesSupportProtocol:")]
        bool DoesSupportProtocol (string protocol);
    }

    public partial interface GCKMimeData {

        [Field ("kGCKApplicationSessionMinBufferSize", "__Internal")]
        int GCKApplicationSessionMinBufferSize { get; }

        [Field ("kGCKApplicationSessionDefaultBufferSize", "__Internal")]
        int GCKApplicationSessionDefaultBufferSize { get; }
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKApplicationSession : GCKApplicationChannelDelegate {

        [Export ("delegate")]
        GCKApplicationSessionDelegate Delegate { get; set; }

        [Export ("applicationInfo")]
        GCKApplicationMetadata ApplicationInfo { get; }

        [Export ("channel")]
        GCKApplicationChannel Channel { get; }

        [Export ("hasStarted")]
        bool HasStarted { get; }

        [Export ("bufferSize")]
        int BufferSize { get; }

        [Export ("stopApplicationWhenSessionEnds")]
        bool StopApplicationWhenSessionEnds { get; set; }

        [Export ("initWithContext:device:bufferSize:")]
        IntPtr Constructor (GCKContext context, GCKDevice device, int bufferSize);

        [Export ("initWithContext:device:")]
        IntPtr Constructor (GCKContext context, GCKDevice device);

        [Export ("startSession")]
        bool StartSession { get; }

        [Export ("startSessionWithApplication:")]
        bool StartSessionWithApplication (string applicationName);

        [Export ("startSessionWithApplication:argument:")]
        bool StartSessionWithApplication (string applicationName, GCKMimeData argument);

        [Export ("endSession")]
        bool EndSession { get; }

        [Export ("resumeSession")]
        bool ResumeSession { get; }
    }

    [Model, BaseType(typeof(NSObject))]
    public partial interface GCKApplicationSessionDelegate {

        [Export ("applicationSessionDidStart")]
        void ApplicationSessionDidStart ();

        [Export ("applicationSessionDidFailToStartWithError:")]
        void ApplicationSessionDidFailToStart (GCKApplicationSessionError error);

        [Export ("applicationSessionDidEndWithError:")]
        void  ApplicationSessionDidEnd(GCKApplicationSessionError error);

		[Field ("kGCKApplicationSessionErrorDomain", "__Internal")]
        NSString GCKApplicationSessionErrorDomain { get; }
    }

    [BaseType (typeof (NSError))]
    public partial interface GCKError {

        [Export ("causedByError")]
        NSError CausedByError { get; }

        [Export ("initWithDomain:code:causedByError:")]
        IntPtr Constructor (string domain, int code, NSError error);

        [Export ("initWithDomain:code:causedByError:additionalUserInfo:")]
        IntPtr Constructor (string domain, int code, NSError error, NSDictionary additionalUserInfo);

        [Static, Export ("localizedDescriptionForCode:")]
        string LocalizedDescriptionForCode (int code);

		[Field ("kGCKApplicationSessionErrorCodeFailedToStartApplication", "__Internal")]
        int GCKApplicationSessionErrorCodeFailedToStartApplication { get; }

		[Field ("kGCKApplicationSessionErrorCodeFailedToQueryApplication", "__Internal")]
        int GCKApplicationSessionErrorCodeFailedToQueryApplication { get; }

		[Field ("kGCKApplicationSessionErrorCodeApplicationStopped", "__Internal")]
        int GCKApplicationSessionErrorCodeApplicationStopped { get; }

		[Field ("kGCKApplicationSessionErrorCodeFailedToCreateChannel", "__Internal")]
        int GCKApplicationSessionErrorCodeFailedToCreateChannel { get; }

		[Field ("kGCKApplicationSessionErrorCodeFailedToConnectChannel", "__Internal")]
        int GCKApplicationSessionErrorCodeFailedToConnectChannel { get; }

		[Field ("kGCKApplicationSessionErrorCodeChannelDisconnected", "__Internal")]
        int GCKApplicationSessionErrorCodeChannelDisconnected { get; }

		[Field ("kGCKApplicationSessionErrorCodeFailedToStopApplication", "__Internal")]
        int GCKApplicationSessionErrorCodeFailedToStopApplication { get; }

		[Field ("kGCKApplicationSessionErrorCodeUnknownError", "__Internal")]
        int GCKApplicationSessionErrorCodeUnknownError { get; }
    }

    [BaseType (typeof (GCKError))]
    public partial interface GCKApplicationSessionError {

        [Export ("initWithCode:causedByError:")]
        IntPtr Constructor (int code, NSError error);

        [Static, Export ("localizedDescriptionForCode:")]
        string LocalizedDescriptionForCode (int code);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKDeviceManager {

        [Export ("devices")]
        NSObject [] Devices { get; }

        [Export ("hasDiscoveredDevices")]
        bool HasDiscoveredDevices { get; }

        [Export ("context")]
        GCKContext Context { get; }

        [Export ("scanning")]
        bool Scanning { get; }

        [Export ("initWithContext:")]
        IntPtr Constructor (GCKContext context);

        [Export ("startScan")]
        void StartScan ();

        [Export ("stopScan")]
        void StopScan ();

        [Export ("addListener:")]
        void AddListener (GCKDeviceManagerListener listener);

        [Export ("removeListener:")]
        void RemoveListener (GCKDeviceManagerListener listener);
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKDeviceManagerListener {

        [Export ("scanStarted")]
        void ScanStarted ();

        [Export ("scanStopped")]
        void ScanStopped ();

        [Export ("deviceDidComeOnline:")]
        void DeviceDidComeOnline (GCKDevice device);

        [Export ("deviceDidGoOffline:")]
        void DeviceDidGoOffline (GCKDevice device);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKApplicationSupportFilterListener : GCKDeviceManagerListener {

        [Export ("initWithContext:applicationName:protocols:downstreamListener:")]
        IntPtr Constructor (GCKContext context, string applicationName, NSObject [] protocols, GCKDeviceManagerListener listener);

//        [Export ("scanStarted")]
//        void ScanStarted ();
//
//        [Export ("deviceDidComeOnline:")]
//        void DeviceDidComeOnline (GCKDevice device);
//
//        [Export ("deviceDidGoOffline:")]
//        void DeviceDidGoOffline (GCKDevice device);
//
//        [Export ("scanStopped")]
//        void ScanStopped ();
    }

    [Category, BaseType (typeof (NSData))]
    public partial interface GCKBase64_NSData {

        [Export ("gck_dataWithBase64EncodedString:")]
        NSData Gck_dataWithBase64EncodedString (string str);

        [Export ("gck_base64EncodedString")]
        string GetGck_base64EncodedString();

        [Export ("gck_base64EncodedStringWithWrapWidth:")]
        string Gck_base64EncodedStringWithWrapWidth (int width);
    }

    [Category, BaseType (typeof (NSString))]
    public partial interface GCKBase64_NSString {

        [Export ("gck_stringWithBase64EncodedString:")]
        string Gck_stringWithBase64EncodedString (string str);

        [Static, Export ("gck_base64EncodedString")]
        string GetGck_base64EncodedString();

        [Export ("gck_base64EncodedStringWithWrapWidth:")]
        string Gck_base64EncodedStringWithWrapWidth (int wrapWidth);

        [Export ("gck_base64DecodedString")]
        string GetGck_base64DecodedString();

        [Export ("gck_base64DecodedData")]
        NSData GetGck_base64DecodedData();
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKContentMetadata {

        [Export ("title")]
        string Title { get; set; }

        [Export ("imageURL")]
        NSUrl ImageURL { get; set; }

        [Export ("contentInfo")]
        NSDictionary ContentInfo { get; set; }

        [Export ("initWithTitle:imageURL:contentInfo:")]
        IntPtr Constructor (string title, NSUrl imageURL, NSDictionary contentInfo);

		[Field ("kGCKVersion", "__Internal")]
        NSString GCKVersion { get; }
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKContext {

        [Export ("userAgent")]
        string UserAgent { get; }

        [Export ("initWithUserAgent:")]
        IntPtr Constructor (string userAgent);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKDevice {

        [Export ("ipAddress")]
        string IpAddress { get; }

        [Export ("deviceID")]
        string DeviceID { get; set; }

        [Export ("friendlyName")]
        string FriendlyName { get; set; }

        [Export ("manufacturer")]
        string Manufacturer { get; set; }

        [Export ("modelName")]
        string ModelName { get; set; }

        [Export ("applicationURL")]
        NSUrl ApplicationURL { get; set; }

        [Export ("icons")]
        NSObject [] Icons { get; set; }

        [Export ("initWithIPAddress:")]
        IntPtr Constructor (string ipAddress);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKDeviceIcon {

        [Export ("width")]
        int Width { get; }

        [Export ("height")]
        int Height { get; }

        [Export ("depth")]
        int Depth { get; }

        [Export ("url")]
        NSUrl Url { get; }

        [Export ("initWithWidth:height:depth:url:")]
        IntPtr Constructor (int width, int height, int depth, NSUrl url);
    }

    [BaseType (typeof (GCKError))]
    public partial interface GCKNetworkRequestError {

        [Export ("initWithCode:causedByError:")]
        IntPtr Constructor (int code, NSError error);

        [Static, Export ("localizedDescriptionForCode:")]
        string LocalizedDescriptionForCode (int code);
    }

    public partial interface GCKMimeData {

		[Field ("kGCKNetworkRequestDefaultTimeout", "__Internal")]
        double GCKNetworkRequestDefaultTimeout { get; }

		[Field ("kGCKNetworkRequestHTTPOriginValue", "__Internal")]
        NSString GCKNetworkRequestHTTPOriginValue { get; }
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKNetworkRequestDelegate {

        [Export ("networkRequestDidComplete:")]
        void  DidComplete(GCKNetworkRequest request);

        [Export ("networkRequest:didFailWithError:")]
        void DidFailWithError (GCKNetworkRequest request, GCKNetworkRequestError error);

        [Export ("networkRequestWasCancelled:")]
        void  WasCancelled(GCKNetworkRequest request);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKNetworkRequest {

        [Export ("delegate")]
        GCKNetworkRequestDelegate Delegate { get; set; }

        [Export ("responseEncoding")]
        NSStringEncoding ResponseEncoding { get; set; }

        [Export ("initWithContext:")]
        IntPtr Constructor (GCKContext context);

        [Export ("execute")]
        void Execute ();

        [Export ("cancel")]
        void Cancel ();

        [Export ("performHTTPGet:timeout:")]
        void PerformHTTPGet (NSUrl url, double timeout);

        [Export ("performHTTPPost:data:timeout:")]
        void PerformHTTPPost (NSUrl url, GCKMimeData data, double timeout);

        [Export ("performHTTPDelete:timeout:")]
        void PerformHTTPDelete (NSUrl url, double timeout);

        [Export ("processResponseWithStatus:finalURL:headers:data:")]
        int ProcessResponseWithStatus (int status, NSUrl finalURL, NSDictionary headers, GCKMimeData data);

        [Export ("parseJson:")]
        NSObject ParseJson (string json);

        [Export ("writeJson:")]
        string WriteJson (NSObject obj);

        [Export ("didComplete")]
        void DidComplete ();

        [Export ("didFailWithError:")]
        void DidFailWithError (GCKError error);
    }

    [BaseType (typeof (GCKNetworkRequest))]
    public partial interface GCKFetchImageRequest {

        [Export ("image")]
        UIImage Image { get; }

        [Export ("initWithContext:url:preferredWidth:preferredHeight:")]
        IntPtr Constructor (GCKContext context, NSUrl url, int width, int height);

        [Export ("initWithContext:url:")]
        IntPtr Constructor (GCKContext context, NSUrl url);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKJsonUtils {

        [Static, Export ("parseJson:")]
        NSObject ParseJson (string json);

        [Static, Export ("writeJson:")]
        string WriteJson (NSObject obj);

        [Static, Export ("isJsonString:equivalentTo:")]
        bool IsJsonString (string actual, string expected);

		[Field ("kGCKMediaProtocolCommandTypeInfo", "__Internal")]
        NSString GCKMediaProtocolCommandTypeInfo { get; }

		[Field ("kGCKMediaProtocolCommandTypeLoad", "__Internal")]
        NSString GCKMediaProtocolCommandTypeLoad { get; }

		[Field ("kGCKMediaProtocolCommandTypePlay", "__Internal")]
        NSString GCKMediaProtocolCommandTypePlay { get; }

		[Field ("kGCKMediaProtocolCommandTypeStop", "__Internal")]
        NSString GCKMediaProtocolCommandTypeStop { get; }

		[Field ("kGCKMediaProtocolCommandTypeVolume", "__Internal")]
        NSString GCKMediaProtocolCommandTypeVolume { get; }

		[Field ("kGCKMediaProtocolCommandTypeSelectTracks", "__Internal")]
        NSString GCKMediaProtocolCommandTypeSelectTracks { get; }

		[Field ("kGCKMediaProtocolErrorDomain", "__Internal")]
        NSString GCKMediaProtocolErrorDomain { get; }

		[Field ("kGCKMediaProtocolErrorInvalidPlayerState", "__Internal")]
        int GCKMediaProtocolErrorInvalidPlayerState { get; }

		[Field ("kGCKMediaProtocolErrorFailedToLoadMedia", "__Internal")]
        int GCKMediaProtocolErrorFailedToLoadMedia { get; }

		[Field ("kGCKMediaProtocolErrorMediaLoadCancelled", "__Internal")]
        int GCKMediaProtocolErrorMediaLoadCancelled { get; }

		[Field ("kGCKMediaProtocolErrorInvalidRequest", "__Internal")]
        int GCKMediaProtocolErrorInvalidRequest { get; }
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKMediaProtocolCommand {

        [Export ("delegate")]
        GCKMediaProtocolCommandDelegate Delegate { get; set; }

        [Export ("sequenceNumber")]
        int SequenceNumber { get; }

        [Export ("type")]
        string Type { get; }

        [Export ("cancelled")]
        bool Cancelled { get; }

        [Export ("completed")]
        bool Completed { get; }

        [Export ("hasError")]
        bool HasError { get; set; }

        [Export ("errorDomain")]
        string ErrorDomain { get; set; }

        [Export ("errorCode")]
        int ErrorCode { get; set; }

        [Export ("errorInfo")]
        NSDictionary ErrorInfo { get; set; }

        [Export ("initWithSequenceNumber:type:")]
        IntPtr Constructor (int sequenceNumber, string type);

        [Export ("complete")]
        void Complete ();

        [Export ("cancel")]
        void Cancel ();
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKMediaProtocolCommandDelegate {

        [Export ("mediaProtocolCommandDidComplete:")]
        void  DidComplete(GCKMediaProtocolCommand command);

        [Export ("mediaProtocolCommandWasCancelled:")]
        void  WasCancelled(GCKMediaProtocolCommand command);
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKMessageStream {

        [Export ("namespace")]
        string Namespace { get; }

//        [Export ("messageSink")]
//        GCKMessageSink MessageSink { get; set; }

        [Export ("initWithNamespace:")]
        IntPtr Constructor (string nameSpace);

        [Export ("didAttach")]
        void DidAttach ();

        [Export ("didDetach")]
        void DidDetach ();

        [Export ("didReceiveMessage:")]
        void DidReceiveMessage (NSObject message);

        [Export ("sendMessage:")]
        bool SendMessage (NSObject message);
    }

    [BaseType (typeof (GCKMessageStream))]
    public partial interface GCKMediaProtocolMessageStream {

        [Export ("delegate")]
        GCKMediaProtocolMessageStreamDelegate Delegate { get; set; }

        [Export ("contentID")]
        string ContentID { get; }

        [Export ("contentInfo")]
        NSDictionary ContentInfo { get; }

        [Export ("playerState")]
        ChromeCast.GCKPlayerState PlayerState { get; }

        [Export ("streamPosition")]
        double StreamPosition { get; }

        [Export ("streamDuration")]
        double StreamDuration { get; }

        [Export ("mediaTitle")]
        string MediaTitle { get; }

        [Export ("mediaImageURL")]
        NSUrl MediaImageURL { get; }

        [Export ("volume")]
        double Volume { get; }

        [Export ("muted")]
        bool Muted { get; }

        [Export ("mediaTracks")]
        NSMutableArray MediaTracks { get; }

//        [Export ("init")]
//        IntPtr Constructor ();

        [Export ("loadMediaWithContentID:contentMetadata:")]
        GCKMediaProtocolCommand LoadMediaWithContentID (string contentID, GCKContentMetadata metadata);

        [Export ("loadMediaWithContentID:contentMetadata:autoplay:")]
        GCKMediaProtocolCommand LoadMediaWithContentID (string contentID, GCKContentMetadata metadata, bool autoplay);

        [Export ("resumeStream")]
        GCKMediaProtocolCommand ResumeStream { get; }

        [Export ("playStream")]
        GCKMediaProtocolCommand PlayStream { get; }

        [Export ("playStreamFrom:")]
        GCKMediaProtocolCommand PlayStreamFrom (double position);

        [Export ("stopStream")]
        bool StopStream { get; }

        [Export ("setStreamVolume:")]
        GCKMediaProtocolCommand SetStreamVolume (double volume);

        [Export ("setStreamMuted:")]
        GCKMediaProtocolCommand SetStreamMuted (bool muted);

        [Export ("selectTracksToEnable:andDisable:")]
        GCKMediaProtocolCommand SelectTracksToEnable (NSObject [] tracksToEnable, NSObject [] tracksToDisable);

        [Export ("requestStatus")]
        GCKMediaProtocolCommand RequestStatus { get; }

        [Export ("sendKeyResponseForRequestID:withTokens:")]
        bool SendKeyResponseForRequestID (int requestID,  NSObject [] tokens);

        [Export ("cancelCommand:")]
        bool CancelCommand (GCKMediaProtocolCommand command);

        [Export ("keyRequestWasReceivedWithRequestID:method:requests:")]
        void KeyRequestWasReceivedWithRequestID (int requestID, string method, NSObject [] requests);

        [Export ("didReceiveMessage:")]
        void DidReceiveMessage (NSObject message);

        [Export ("didDetach")]
        void DidDetach ();
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKMediaProtocolMessageStreamDelegate {

        [Export ("mediaProtocolMessageStreamDidReceiveStatusUpdate:")]
        void  DidReceiveStatusUpdate(GCKMediaProtocolMessageStream stream);

        [Export ("mediaProtocolMessageStreamDidUpdateTrackList:")]
        void  DidUpdateTrackList(GCKMediaProtocolMessageStream stream);

        [Export ("mediaProtocolMessageStream:didReceiveErrorWithDomain:code:errorInfo:")]
        void DidReceiveErrorWithDomain (GCKMediaProtocolMessageStream stream, string domain, int code, NSDictionary errorInfo);
    }

  
    [BaseType (typeof (NSObject))]
    public partial interface GCKMediaTrack {

        [Export ("initWithIdentifier:type:name:languageCode:enabled:")]
        IntPtr Constructor (int identifier, GCKMediaTrackType type, string name, string languageCode, bool enabled);

        [Export ("identifier")]
        int Identifier { get; }

        [Export ("type")]
        GCKMediaTrackType Type { get; }

        [Export ("name")]
        string Name { get; }

        [Export ("languageCode")]
        string LanguageCode { get; }

        [Export ("enabled")]
        bool Enabled { get; set; }
    }


    [BaseType (typeof (NSObject))]
    public partial interface GCKMimeData {

        [Export ("type")]
        string Type { get; }

        [Export ("data")]
        NSData Data { get; }

        [Export ("textData")]
        string TextData { get; }

        [Export ("initWithData:type:")]
        IntPtr Constructor (NSData data, string type);

        [Export ("initWithTextData:type:")]
        IntPtr Constructor (string data, string type);

        [Static, Export ("mimeDataWithJsonObject:")]
        GCKMimeData MimeDataWithJsonObject (NSObject json);

        [Static, Export ("mimeDataWithURL:")]
        GCKMimeData MimeDataWithURL (NSUrl url);
    }

    [Category, BaseType (typeof (NSDictionary))]
    public partial interface GCKTypedValueLookup_NSDictionary {

        [Export ("gck_stringForKey:withDefaultValue:")]
        string Gck_stringForKey (string key, string defaultValue);

        [Export ("gck_stringForKey:")]
        string Gck_stringForKey (string key);

        [Export ("gck_integerForKey:withDefaultValue:")]
        int Gck_integerForKey (string key, int defaultValue);

        [Export ("gck_integerForKey:")]
        int Gck_integerForKey (string key);

        [Export ("gck_doubleForKey:withDefaultValue:")]
        double Gck_doubleForKey (string key, double defaultValue);

        [Export ("gck_doubleForKey:")]
        double Gck_doubleForKey (string key);

        [Export ("gck_boolForKey:withDefaultValue:")]
        bool Gck_boolForKey (string key, bool defaultValue);

        [Export ("gck_boolForKey:")]
        bool Gck_boolForKey (string key);

        [Export ("gck_setStringValue:forKey:")]
        void Gck_setStringValue (string value, string key);

        [Export ("gck_setIntegerValue:forKey:")]
        void Gck_setIntegerValue (int value, string key);

        [Export ("gck_setDoubleValue:forKey:")]
        void Gck_setDoubleValue (double value, string key);

        [Export ("gck_setBoolValue:forKey:")]
        void Gck_setBoolValue (bool value, string key);
    }

    [Category, BaseType (typeof (NSString))]
    public partial interface GCKPatternMatching_NSString {

        [Export ("gck_matchesPattern:")]
        bool Gck_matchesPattern (string regexPattern);
    }

    public partial interface GCKMimeData {

		[Field ("kGCKHTTPStatusOK", "__Internal")]
        int GCKHTTPStatusOK { get; }

		[Field ("kGCKHTTPStatusCreated", "__Internal")]
        int GCKHTTPStatusCreated { get; }

		[Field ("kGCKHTTPStatusNoContent", "__Internal")]
        int GCKHTTPStatusNoContent { get; }

		[Field ("kGCKHTTPStatusForbidden", "__Internal")]
        int GCKHTTPStatusForbidden { get; }

		[Field ("kGCKHTTPStatusNotFound", "__Internal")]
        int GCKHTTPStatusNotFound { get; }

		[Field ("kGCKHTTPStatusNotImplemented", "__Internal")]
        int GCKHTTPStatusNotImplemented { get; }

		[Field ("kGCKHTTPStatusServiceUnavailable", "__Internal")]
        int GCKHTTPStatusServiceUnavailable { get; }

		[Field ("kGCKHTTPHeaderLocation", "__Internal")]
        NSString GCKHTTPHeaderLocation { get; }
    }

    [BaseType (typeof (NSObject))]
    public partial interface GCKSimpleHTTPRequest {

        [Export ("delegate")]
        GCKSimpleHTTPRequestDelegate Delegate { get; set; }

        [Export ("timeout")]
        double Timeout { get; set; }

        [Export ("url")]
        NSUrl Url { get; }

        [Export ("finalUrl")]
        NSUrl FinalUrl { get; }

        [Export ("responseHeaders")]
        NSDictionary ResponseHeaders { get; }

//        [Export ("init")]
//        IntPtr Constructor ();

        [Export ("startGetRequest:")]
        void StartGetRequest (NSUrl url);

        [Export ("startPostRequest:data:")]
        void StartPostRequest (NSUrl url, GCKMimeData data);

        [Export ("setValue:forHTTPHeaderField:")]
        void SetValue (string value, string field);

        [Export ("startDeleteRequest:")]
        void StartDeleteRequest (NSUrl url);

        [Export ("cancel")]
        void Cancel ();
    }

    [Model, BaseType (typeof (NSObject))]
    public partial interface GCKSimpleHTTPRequestDelegate {

//        [Export ("configureURLRequest:forSimpleHTTPRequest:")]
//        void ForSimpleHTTPRequest (NSMutableURLRequest request, GCKSimpleHTTPRequest simpleRequest);

        [Export ("httpRequest:didCompleteWithStatusCode:finalURL:headers:data:")]
        void DidCompleteWithStatusCode (GCKSimpleHTTPRequest request, int status, NSUrl finalURL, NSDictionary headers, GCKMimeData data);

        [Export ("httpRequest:didFailWithError:")]
        void DidFailWithError (GCKSimpleHTTPRequest request, NSError error);
    }

    [BaseType (typeof (GCKError))]
    public partial interface GCKWebSocketError {

        [Export ("initWithCode:")]
        IntPtr Constructor (int code);

        [Static, Export ("localizedDescriptionForCode:")]
        string LocalizedDescriptionForCode (int code);
    }

}

