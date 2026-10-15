/****************
Ma sinh vien: 202418889
Ho ten: Đặng Thị Hương Giang
****************/
// Lớp cha chung cho mỗi thiết bị

public abstract class Device
{
    public string MaThietBi { get; }
    public string TenThietBi { get; set; }
    public int NamSuDung { get; set; }
    public decimal GiaMua { get; set; }
    public DeviceStatus TrangThai { get; set; }

    public Device(string ma, string ten, int nam, decimal gia)
    {
        if (string.IsNullOrWhiteSpace(ma))
            throw new ArgumentException("Ma thiet bi khong duoc rong");
        if (gia <= 0)
            throw new ArgumentException("Gia mua phai lon hon 0");
        if (nam > DateTime.Now.Year)
            throw new ArgumentException("Nam su dung khong duoc lon hon nam hien tai");

        MaThietBi = ma;
        TenThietBi = ten;
        NamSuDung = nam;
        GiaMua = gia;
        TrangThai = DeviceStatus.Available;
    }

    public int SoNamDaDung()
    {
        return DateTime.Now.Year - NamSuDung;
    }

    public abstract decimal TinhPhiBaoTriNam();

    public override string ToString()
    {
        return MaThietBi + " - " + TenThietBi + " - " + TrangThai;
    }
}
