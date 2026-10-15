/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang
****************/
// Người mượn có 2 loại: giảng viên và trợ giảng

public interface IBorrower
{
    string BorrowerId { get; }
    string FullName { get; }
    int GetMaximumDevices(); // số thiết bị tối đa trong 1 phiếu
}

// Giảng viên được mượn tối đa 3 thiết bị
public class Lecturer : IBorrower
{
    public string BorrowerId { get; set; }
    public string FullName { get; set; }

    public Lecturer(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices()
    {
        return 3;
    }
}

// Trợ giảng được mượn tối đa 2 thiết bị
public class TeachingAssistant : IBorrower
{
    public string BorrowerId { get; set; }
    public string FullName { get; set; }

    public TeachingAssistant(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices()
    {
        return 2;
    }
}
