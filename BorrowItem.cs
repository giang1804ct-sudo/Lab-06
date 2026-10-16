/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang

****************/
// 1 dòng mượn trong phiếu

public class BorrowItem
{
    public TeachingDevice Device { get; set; }
    public DateTime BorrowedDate { get; set; }
    public int FromPeriod { get; set; }
    public int ToPeriod { get; set; }
    public DateTime? ReturnedDate { get; private set; }

    // Nếu đã có ngày trả thì coi như đã trả
    public bool IsReturned
    {
        get { return ReturnedDate != null; }
    }

    public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate)
    {
        // FIX 1: check null
        if (device == null)
            throw new ArgumentException("Device khong duoc null");

        // FIX 2: hạn trả phải sau ngày mượn
        if (dueDate < borrowedDate)
            throw new InvalidBorrowPeriodException(device.MaThietBi, 0, device.MaxBorrowDays);

        Device = device;
        BorrowedDate = borrowedDate;
        // Từ tiết 1 đến tiết 5 cho đơn giản
        FromPeriod = 1;
        ToPeriod = 5;
    }

    public void Return(DateTime returnedDate)
    {
        // FIX 3: không cho trả 2 lần
        if (IsReturned)
            throw new DeviceAlreadyReturnedException("Thiet bi " + Device.MaThietBi + " da tra roi");

        // FIX 4: ngày trả không được trước ngày mượn
        if (returnedDate < BorrowedDate)
            throw new ArgumentException("Ngay tra khong duoc truoc ngay muon");

        ReturnedDate = returnedDate;
    }
}
