create view		dbo.vi_assu as 
select			ob.* 
from			dbo.vi_obce as ob
union select	os.*
from			dbo.vi_ostatne as os