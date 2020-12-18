select top 5 * from 
mos_patient 
where substring(addr,0,3)='KP' 
and SUBSTRING(name,0,2)='W' 
and gender=@gen;