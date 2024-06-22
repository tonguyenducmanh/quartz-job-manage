Solution lập trình việc tạo job cho worker chạy và remove job của worker.
Job được chạy bằng thư viện Quartz.NET
Các Job sẽ được lưu trữ trong database PostgreSQL

solution sẽ tạo ra 3 job khác nhau cho worker chạy
mỗi job sẽ chạy vô hạn lần, cách nhau 3 giây sẽ chạy lại

![demo kết quả](Img/Demo.png)

Danh sách lệnh đã tạo trong database postgreSQL
![Danh sách lệnh trong DB](Img/JobInDatabase.png)

khi start solution sẽ mở ra 1 giao diện danh sách api, trong này gồm có đầu api tạo lệnh và xóa lệnh
nhập enum của job muốn tạo và xóa
![API tạo và xóa lệnh](Img/ApiList.png)

2 project chính của solution là Job.API dùng để tạo và check hob và Job.Worker dùng để thực thi job
![solution](Img/solution.png)
