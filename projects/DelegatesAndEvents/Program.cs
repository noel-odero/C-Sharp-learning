using System.Reflection;
using EnventsAndDelegates;

var video = new Video("Video 1");
var videoEncoder = new VideoEncoder(); // publisher
var mailService = new MailService(); // subsciber

videoEncoder.VideoEncoded += mailService.OnVideoEncoded;

videoEncoder.Encode(video);

