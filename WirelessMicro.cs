/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Micro không dây

public class WirelessMicro : TeachingDevice
{
    public WirelessMicro(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 1, 50000) // chỉ được mượn 1 ngày
    {
    }

    // Phí bảo trì = 15% giá mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.15m;
    }
}
