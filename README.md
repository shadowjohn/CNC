賴Q的 CNC 選刀小程式
<br>
使用方法：<br>
  把 nc 檔或 txt 檔拖拉進去，<br>
  會自動依照 M6Tx 作出選擇器<br>
  選完後儲存，就會寫回原本的檔案<br>
  <br>
  針對 M6Tx x 為 12346 時，預設帶出 +<br>
  若 M6Tx x 為 5 時，預設帶出 -<br>
  <br>
  該值會改變 G1 Z+ 或 G1 Z-
  
<img src="screenshot/screenshot2.jpg">

ChangeLog:
* 2020-07-14 V1.2 *
M6T1 2 3 4 6，會將 G1 Z+、- 改成 G1 Z+
M6T5 會將 G1 Z+、- 改成 G1 Z- 

* 2019-10-31 V1.0 *
將 M6T? 變成可以下拉選擇

 
  
