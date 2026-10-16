/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/


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
        Device = device;
        BorrowedDate = borrowedDate;
        // Từ tiết 1 đến tiết 5 cho đơn giản, em làm cứng
        FromPeriod = 1;
        ToPeriod = 5;
    }

    public void Return(DateTime returnedDate)
    {
       
        ReturnedDate = returnedDate;
    }
}
