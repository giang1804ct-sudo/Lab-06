/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Lớp TeachingDevice kế thừa Device
// Thêm 2 thuộc tính: số ngày mượn tối đa + phí trễ 1 ngày
// Để xử lý mượn/trả trong phiếu

public abstract class TeachingDevice : Device
{
    public int MaxBorrowDays { get; set; } // số ngày được mượn tối đa
    public decimal LateFeePerDay { get; set; } // tiền phạt 1 ngày trễ

    public TeachingDevice(string ma, string ten, int nam, decimal gia, int maxDays, decimal fee)
        : base(ma, ten, nam, gia)
    {
        MaxBorrowDays = maxDays;
        LateFeePerDay = fee;
    }

    // Đổi trạng thái sang Borrowed
    public void MarkAsBorrowed()
    {
        TrangThai = DeviceStatus.Borrowed;
    }

    // Trả xong thì về Available
    public void MarkAsAvailable()
    {
        TrangThai = DeviceStatus.Available;
    }

    // Tính tiền trễ
    public decimal TinhTienTre(int soNgayTre)
    {
        if (soNgayTre <= 0) return 0;
        return soNgayTre * LateFeePerDay;
    }
}
