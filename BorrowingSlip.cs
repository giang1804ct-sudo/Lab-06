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

    // Hàm kiểm tra riêng, cho code gọn
    private void ValidateBorrowing(TeachingDevice device, int borrowDays)
    {
        if (device == null)
            throw new ArgumentException("Device khong duoc null");

        // Số ngày phải trong khoảng cho phép
        if (borrowDays <= 0)
            throw new InvalidBorrowPeriodException(device.MaThietBi, borrowDays, device.MaxBorrowDays);

        if (borrowDays > device.MaxBorrowDays)
            throw new InvalidBorrowPeriodException(device.MaThietBi, borrowDays, device.MaxBorrowDays);

        // Check trùng trong phiếu
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Device.MaThietBi == device.MaThietBi)
                throw new DuplicateDeviceException("Thiet bi " + device.MaThietBi + " da co trong phieu");
        }

        // Check giới hạn GV 3 / TG 2
        if (items.Count >= Borrower.GetMaximumDevices())
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());

        // Chỉ Available mới cho mượn
        if (device.TrangThai != DeviceStatus.Available)
            throw new DeviceUnavailableException(device.MaThietBi, device.TrangThai);
    }

    // FIX quan trọng: validate trước, đổi trạng thái sau
    public void AddDevice(TeachingDevice device, int borrowDays)
    {
        // Check hết lỗi trước
        ValidateBorrowing(device, borrowDays);

        BorrowItem it = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));
        device.MarkAsBorrowed(); // đổi trạng thái sau khi check xong
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
            throw new DeviceNotInSlipException("Thiet bi " + deviceId + " khong thuoc phieu");

        // FIX: check trả 2 lần
        if (found.IsReturned)
            throw new DeviceAlreadyReturnedException("Thiet bi " + deviceId + " da tra truoc do");

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
                DateTime ngayTra = items[i].ReturnedDate ?? items[i].BorrowedDate;
                int tre = (ngayTra - han).Days;
                if (tre > 0)
                    tong = tong + items[i].Device.TinhTienTre(tre);
            }
        }
        return tong;
    }
}
