/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Điều khiển máy chiếu

public class ProjectorRemote : TeachingDevice
{
    public ProjectorRemote(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 7, 20000) // được mượn tối đa 7 ngày
    {
    }

    // Phí bảo trì = 10% giá mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.1m;
    }
}
