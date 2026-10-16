/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang

****************/
// Lớp phòng học, giữ danh sách thiết bị

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

    // FIX 1+2: check null + trùng mã
    public void AddDevice(Device device)
    {
        // Không cho null, tránh crash
        if (device == null)
            throw new ArgumentException("Thiet bi khong duoc null");

        // Không cho 2 máy cùng mã
        if (FindDevice(device.MaThietBi) != null)
            throw new DuplicateDeviceException("Trung ma thiet bi: " + device.MaThietBi);

        ds.Add(device);
    }

    public bool RemoveDevice(string deviceId)
    {
        Device? d = FindDevice(deviceId);
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

    // FIX 3: dùng đa hình, gọi trực tiếp TinhPhiBaoTriNam()
    public decimal CalculateAnnualMaintenanceCost()
    {
        decimal tong = 0;
        for (int i = 0; i < ds.Count; i++)
        {
            // Gọi đa hình, thêm loại mới không phải sửa if-else
            tong = tong + ds[i].TinhPhiBaoTriNam();
        }
        return tong;
    }

    // FIX 4: thêm điều kiện dùng trên 5 năm
    public List<Device> GetDevicesRequiringMaintenance()
    {
        List<Device> kq = new List<Device>();
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].TrangThai == DeviceStatus.UnderMaintenance || ds[i].SoNamDaDung() > 5)
                kq.Add(ds[i]);
        }
        return kq;
    }
}
