/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// File ngoại lệ chung

public class BorrowingException : Exception
{
    public BorrowingException(string message) : base(message)
    {
    }

    public BorrowingException(string message, Exception inner) : base(message, inner)
    {
    }
}

// Thiết bị không sẵn sàng thì không cho mượn
public class DeviceUnavailableException : BorrowingException
{
    public string DeviceId { get; }
    public DeviceStatus CurrentStatus { get; }

    public DeviceUnavailableException(string id, DeviceStatus st)
        : base("Thiết bị " + id + " đang ở trạng thái " + st + " nên không mượn được")
    {
        DeviceId = id;
        CurrentStatus = st;
    }
}

// Vượt quá số lượng được mượn
public class BorrowLimitExceededException : BorrowingException
{
    public string BorrowerId { get; }
    public int MaximumDevices { get; }

    public BorrowLimitExceededException(string borrowerId, int max)
        : base("Người " + borrowerId + " chỉ được mượn tối đa " + max + " thiết bị")
    {
        BorrowerId = borrowerId;
        MaximumDevices = max;
    }
}

// Số ngày mượn không hợp lệ
public class InvalidBorrowPeriodException : BorrowingException
{
    public string DeviceId { get; }
    public int RequestedDays { get; }
    public int MaxDays { get; }

    public InvalidBorrowPeriodException(string deviceId, int req, int max)
        : base("Số ngày mượn " + req + " không hợp lệ, tối đa " + max)
    {
        DeviceId = deviceId;
        RequestedDays = req;
        MaxDays = max;
    }
}

public class DuplicateDeviceException : BorrowingException
{
    public DuplicateDeviceException(string msg) : base(msg) { }
}

public class DeviceNotInSlipException : BorrowingException
{
    public DeviceNotInSlipException(string msg) : base(msg) { }
}

public class DeviceAlreadyReturnedException : BorrowingException
{
    public DeviceAlreadyReturnedException(string msg) : base(msg) { }
}
