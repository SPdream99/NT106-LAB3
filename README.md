# LAB 3 NT106.Q14 - Lập trình Sockets trong C#

Bài tập trên GitHub để nộp cho giáo viên lớp NT106.Q14 UIT VNUHCM 2025

- Lớp học: NT106.Q14
- Giáo viên hướng dẫn: ThS. Lê Minh Khánh Hội

## THÔNG TIN CÁC THÀNH VIÊN
| Tên thành viên   |      MSSV      |
|------------------|:--------------:|
| Nguyễn Nhật Anh  |   24520112     |
| Nguyễn Hoàng Anh  |   24520102     |
| SV1  |   2XXXXXX     |
| SV2  |   2XXXXXX     |
| SV3   |   2XXXXXX     |

## Phần mềm cần thiết

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Hướng dẫn cài đặt

### 1. Tải và cài đặt các phần mềm cần thiết

### 2. Cài đầy đủ thành phần cho VS2022
#### Workloads:
  
   * ".NET desktop development"
   * "Data storage and processing"

### 3. Tạo bản sao của repository NT106-LAB3
```bash
git clone https://github.com/SPdream99/NT106-LAB3/
cd "NT106-LAB3\Lab3"
start Lab3.slnx
```

### 4. Build và chạy Lab03
* Set Lab3 thành Startup Project
* Debug -> Start Debugging

### 5. Sử dụng ứng dụng
Từ Lab03, chọn bài muốn test

| Bài      | Tên Ứng dụng                               | Mô tả ngắn gọn                                                                                                                                                                                             |
| :------- | :----------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Bài 1    | UDP Client & Server                        | Viết ứng dụng gửi và nhận dữ liệu giữa hai bên (Client và Server) sử dụng giao thức UDP. Client sẽ chỉ định IP, port và thông điệp để gửi đến Server.                                 |
| Bài 2    | Telnet TCP Listener                        | Viết chương trình lắng nghe dữ liệu từ dịch vụ Telnet bằng kết nối TCP, sử dụng lớp Socket. Khi người dùng telnet đến IP và port 8080 của máy, chương trình sẽ nhận và hiển thị thông điệp được gõ vào. |
| Bài 3    | TCP (1 Server - 1 Client)                  | Viết ứng dụng gửi và nhận dữ liệu giữa 1 Server và 1 Client sử dụng giao thức TCP (TCP Client và TCP Listener). Server sẽ lắng nghe kết nối, và Client gửi thông điệp đến, Server nhận và hiển thị thông điệp đó. |
| Bài 4    | Quản lý phòng vé (1 Server - Multi Client) | Kế thừa từ bài thực hành số 2. Dữ liệu vé được lưu trữ và đồng bộ tại Server. Nếu một vé đã được đặt ở một quầy (Client), các quầy khác sẽ không đặt được vé đó nữa.              |
| Bài 5    | Hôm nay ăn gì? (1 Server - Multi Client)   | Kế thừa từ bài thực hành số 2. Dữ liệu được người dùng cập nhật và lưu trữ tại CSDL SQLite của Server. Bổ sung tính năng cho phép người dùng chọn ngẫu nhiên 1 món ăn từ dữ liệu của bản thân hoặc của cộng đồng.  |
| Bài 6    | Chat Room (1 Server - Multi Client)        | Viết chương trình Chat Room sử dụng TCP. Server quản lý các kết nối và "broadcast" tin nhắn. Khi một người dùng gửi tin nhắn, tất cả mọi người còn lại đều nhận được. Có thêm tính năng nhắn tin riêng và mở rộng gửi file/hình ảnh.        |

## Tài liệu tham khảo

* [Tài liệu C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Tài liệu Winforms](https://learn.microsoft.com/vi-vn/dotnet/desktop/winforms/)
* Tài liệu của giáo viên cung cấp trong lớp NT106.Q14

### CC BY-NC 4.0
