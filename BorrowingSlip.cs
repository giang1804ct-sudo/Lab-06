/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Phiếu mượn, 1 người mượn nhiều thiết bị

public class BorrowingSlip
{
    public string SlipId { get; set; }
    public IBorrower Borrower { get; set; }
    public string TeachingRoom { get; set; }
    public DateTime BorrowedDate { get; set; }

    private List<BorrowItem> items = new List<BorrowItem>();
    public IReadOnlyList<BorrowItem> Items
    {
        get { return items; }
    }

    public BorrowingSlip(string id, IBorrower borrower, string room, DateTime date)
    {
        SlipId = id;
        Borrower = borrower;
        TeachingRoom = room;
        BorrowedDate = date;
    }

    // Thêm thiết bị vào phiếu 
    public void AddDevice(TeachingDevice device, int borrowDays)
    {
        
        device.MarkAsBorrowed();

        if (items.Count >= Borrower.GetMaximumDevices())
        {
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
        }

        BorrowItem it = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));
        items.Add(it);
    }

    // Trả thiết bị
    public decimal ReturnDevice(string deviceId, DateTime returnedDate)
    {
        BorrowItem? found = null;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Device.MaThietBi == deviceId)
                found = items[i];
        }
        if (found == null)
            throw new DeviceNotInSlipException("Thiết bị " + deviceId + " không thuộc phiếu");

        found.Return(returnedDate);
        found.Device.MarkAsAvailable();

        // tính tiền trễ đơn giản
        DateTime hanTra = found.BorrowedDate.AddDays(found.Device.MaxBorrowDays);
        int tre = (returnedDate - hanTra).Days;
        if (tre < 0) tre = 0;
        return found.Device.TinhTienTre(tre);
    }

    public decimal CalculateTotalLateFee()
    {
        decimal tong = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsReturned)
            {
                DateTime han = items[i].BorrowedDate.AddDays(items[i].Device.MaxBorrowDays);
                int tre = (((DateTime)items[i].ReturnedDate) - han).Days;
                if (tre > 0)
                    tong = tong + items[i].Device.TinhTienTre(tre);
            }
        }
        return tong;
    }
}
