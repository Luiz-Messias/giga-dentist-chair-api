namespace projeto_giga.Domain.Entities;

public class Allocation : Entity
{
    public int ChairId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public Allocation(int chairId, DateTime startTime, DateTime endTime)
    {
        ChairId = chairId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void SetChairId(int chairId)
    {
        if (chairId <= 0)
            throw new ArgumentException("Chair ID must be greater than zero.");

        ChairId = chairId;
    }

    public void SetStartTime(DateTime startTime)
    {
        if (startTime >= EndTime)
            throw new ArgumentException("Start time must be before end time.");
        
        StartTime = startTime;
    }

    public void SetEndTime(DateTime endTime)
    {
        if (endTime <= StartTime)
            throw new ArgumentException("End time must be after start time.");
        
        EndTime = endTime;
    }
}