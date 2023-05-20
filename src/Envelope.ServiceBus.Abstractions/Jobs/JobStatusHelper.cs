using Envelope.Enums;
using Envelope.ServiceBus.Queries;

namespace Envelope.ServiceBus.Jobs;

public static class JobStatusHelper
{
	public static JobStatus GetJobActivityStatus(IDbJob job)
	{
		if (job.Status == (int)JobStatus.Disabled || job.Status == (int)JobStatus.Stopped)
			return EnumHelper.ConvertIntToEnum<JobStatus>(job.Status);

		var nowUtc = DateTime.UtcNow;

		if (!job.LastExecutionStartedUtc.HasValue)
		{
			if (job.NextExecutionRunUtc.HasValue)
			{
				if (job.NextExecutionRunUtc < nowUtc)
				{
					return JobStatus.Offline;
				}
				else if (job.Status == (int)JobStatus.InProcess)
				{
					return JobStatus.InProcess;
				}
				else
				{
					return JobStatus.Idle;
				}
			}
			else //NextExecutionRunUtc.HasValue == false
			{
				return JobStatus.Stopped;
			}
		}
		else //LastExecutionStartedUtc.HasValue == true
		{
			if (0 < job.DeclaringAsOfflineAfterMinutesOfInactivity
				&& job.LastExecutionStartedUtc.Value.AddMinutes(job.DeclaringAsOfflineAfterMinutesOfInactivity) < nowUtc)
			{
				return JobStatus.Offline;
			}
			else if (0 < job.ExecutionEstimatedTimeInSeconds
				&& job.LastExecutionStartedUtc.Value.AddSeconds(job.ExecutionEstimatedTimeInSeconds) < nowUtc)
			{
				return JobStatus.TooLongProcessing;
			}
			else
			{
				return EnumHelper.ConvertIntToEnum<JobStatus>(job.Status);
			}
		}
	}
}
