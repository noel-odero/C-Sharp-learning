using System.Reflection;
using EnventsAndDelegates;

var video = new Video("Video 1");
var videoEncoder = new VideoEncoder();

videoEncoder.Encode(video);