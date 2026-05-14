using System.Reflection;
using EnventsAndDelegates;

var video = new Video("Video 1");
var videoEncoder = new VideoEncoder(); // publisher
var mailService = new MailService(); // subsciber
var messageService = new MessageService();

videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

videoEncoder.Encode(video);




