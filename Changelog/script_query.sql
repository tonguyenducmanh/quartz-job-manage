-- toàn bộ các job được tạo ở solution sẽ lưu ở 3 bảng này
select
	*
from
	tdm.qrtz_simple_triggers;

select
	*
from
	tdm.qrtz_triggers;

select
	*
from
	tdm.qrtz_job_details;