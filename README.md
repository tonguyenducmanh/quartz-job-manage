Solution lập trình việc tạo job cho worker chạy và remove job của worker.
Job được chạy bằng thư viện Quartz.NET
Các Job sẽ được lưu trữ trong database PostgreSQL

solution sẽ tạo ra 3 job khác nhau cho worker chạy
mỗi job sẽ chạy vô hạn lần, cách nhau 3 giây sẽ chạy lại

Hướng dẫn chạy project

Bước 1:
Tạo 1 database mới
Dán connection string của postgreSQL local vào config

![alt text](Img/Step1.png)

Bước 2:

Chạy toàn bộ script có trong file sau trong postgresql ở database vừa tạo

![alt text](Img/Step2.png)

Bước 3:

Start 2 project là Job.API và Job.Worker

Bước 4:

chạy 2 api tạo lệnh và xóa lệnh, kiểm tra kết quả ở cửa sổ console của worker

![demo kết quả](Img/Demo.png)

Danh sách lệnh đã tạo trong database postgreSQL

![Danh sách lệnh trong DB](Img/JobInDatabase.png)

khi start solution sẽ mở ra 1 giao diện danh sách api, trong này gồm có đầu api tạo lệnh và xóa lệnh
nhập enum của job muốn tạo, xóa, tạm ngưng, tiếp tục

![danh sách api](Img/swaggerapilist.png)

2 project chính của solution là Job.API dùng để tạo và check job và Job.Worker dùng để thực thi job

![solution](Img/solution.png)

Lưu ý: trong solution này sử dụng các hàm mặc định được cung cấp bởi Quartz.NET để insert và delete job trong database. Không được phép tự viết câu lệnh insert, delete, update cho các database này do có thể gây ra deadlock. Thư viện đã lo vụ này ổn rồi thì cứ dùng

![DeadLockPrevent](Img/DeadLockPrevent.png)

Cấu hình chạy cluster phục vụ cho việc triển khai nhiều pod dựa trên 1 image docker ( phân tải job cho nhiều worker)

![clustering](Img/Clustering.png)

Trường hợp muốn update thông tin của 1 job có sẵn, sử dụng các hàm overload có cờ replace

![upate](Img/UpdateJob.png)
