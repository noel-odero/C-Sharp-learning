namespace OrderFlow.Models;

public class DlqMessage
{
    public string OriginalTopic { get; set; } = string.Empty;
    public int OriginalPartition { get; set; }
    public long OriginalOffset { get; set; }
    public string FailedMessage { get; set; } = string.Empty;
    public string ConsumerGroup { get; set; } = string.Empty;
    public string ErrorReason { get; set; } = string.Empty;
    public DateTimeOffset FailedAt { get; set; }
    public int RetryCount { get; set; }
}