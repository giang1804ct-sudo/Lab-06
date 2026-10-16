/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Lớp phòng học, giữ danh sách thiết bị
// TODO: cần xử lý thêm chỗ này...

public class ClassRoom
{
    public string MaPhong { get; set; }
    public string TenPhong { get; set; }
    public int SucChua { get; set; }
    private List<Device> ds = new List<Device>();

    public ClassRoom(string ma, string ten, int sucChua)
    {
        MaPhong = ma;
        TenPhong = ten;
        SucChua = sucChua;
    }

    // Thêm thiết bị vào phòng
    public void AddDevice(Device device)
    {
        
        ds.Add(device);
    }

    public bool RemoveDevice(string deviceId)
    {
        Device d = FindDevice(deviceId);
        if (d == null) return false;
        ds.Remove(d);
        return true;
    }

    public Device? FindDevice(string deviceId)
    {
        // vòng lặp đơn giản để tìm
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].MaThietBi == deviceId)
                return ds[i];
        }
        return null;
    }


    public decimal CalculateAnnualMaintenanceCost()
    {
        decimal tong = 0;
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i] is ProjectorRemote)
                tong = tong + ds[i].GiaMua * 0.1m;
            else if (ds[i] is AirConditionerRemote)
                tong = tong + ds[i].GiaMua * 0.05m;
            else if (ds[i] is WirelessMicro)
                tong = tong + ds[i].GiaMua * 0.15m;
        }
        return tong;
    }


    public List<Device> GetDevicesRequiringMaintenance()
    {
        List<Device> kq = new List<Device>();
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].TrangThai == DeviceStatus.UnderMaintenance)
                kq.Add(ds[i]);
        }
        // TODO: thầy bảo cần thêm điều kiện trên 5 năm nữa
        return kq;
    }
}
