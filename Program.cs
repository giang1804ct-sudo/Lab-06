/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang

****************/
// Chương trình chính để test

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== LAB 06 ver2 - bản nháp, còn lỗi ===");

        // 11.1 Luồng thành công
        try
        {
            Lecturer gv = new Lecturer("GV01", "Thầy A");
            ProjectorRemote dkMayChieu = new ProjectorRemote("TB01", "ĐK Máy chiếu", 2022, 500000);
            AirConditionerRemote dkDieuHoa = new AirConditionerRemote("TB02", "ĐK Điều hòa", 2021, 300000);
            WirelessMicro micro = new WirelessMicro("TB03", "Micro", 2023, 1000000);

            ClassRoom phong = new ClassRoom("P101", "Phòng 101", 50);
            phong.AddDevice(dkMayChieu);
            phong.AddDevice(dkDieuHoa);
            phong.AddDevice(micro);
            Console.WriteLine("Tiền bảo trì 1 năm: " + phong.CalculateAnnualMaintenanceCost());

            BorrowingSlip phieu = new BorrowingSlip("PH01", gv, "P101", DateTime.Now);
            phieu.AddDevice(dkMayChieu, 5);
            phieu.AddDevice(micro, 1);
            Console.WriteLine("Thêm 2 thiết bị ok");

            decimal tien = phieu.ReturnDevice("TB03", DateTime.Now.AddDays(2));
            Console.WriteLine("Trả micro trễ 2 ngày, tiền phạt: " + tien);

            Console.WriteLine("Tổng tiền trễ: " + phieu.CalculateTotalLateFee());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }

        // 11.2 Luồng ngoại lệ
        Console.WriteLine("\n--- Test ngoại lệ ---");
        try
        {
            TeachingAssistant tg = new TeachingAssistant("TG01", "Trợ giảng B");
            AirConditionerRemote dkHong = new AirConditionerRemote("TB10", "ĐK hỏng", 2020, 200000);
            dkHong.TrangThai = DeviceStatus.UnderMaintenance;

            BorrowingSlip p2 = new BorrowingSlip("PH02", tg, "P102", DateTime.Now);
            p2.AddDevice(dkHong, 2);
            Console.WriteLine("BUG: vẫn cho mượn thiết bị hỏng, đáng lẽ phải ném DeviceUnavailableException");

            WirelessMicro m2 = new WirelessMicro("TB11", "Micro 2", 2022, 800000);
            p2.AddDevice(m2, 1);
            p2.AddDevice(m2, 1);
            Console.WriteLine("BUG: thêm trùng mã vẫn được");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bắt được ngoại lệ: " + ex.GetType().Name + " - " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Xong phần test (finally luôn chạy).");
        }
    }
}
