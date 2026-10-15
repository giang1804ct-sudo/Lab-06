/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Điều khiển điều hòa

public class AirConditionerRemote : TeachingDevice
{
    public AirConditionerRemote(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 3, 10000) // tối đa 3 ngày
    {
    }

    // Phí bảo trì = 5% giá mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.05m;
    }
}
