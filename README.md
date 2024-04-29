# XML文件格式
## 管理员名单.xml
<Employee>
  <Employer>
    <EmployerName>宋江</EmployerName>
    <UserName>gly001</UserName>
    <Password>abc</Password>
    <Date>2023-05-18 18:39:14</Date>
  </Employer>
</Employee>

## 服务员名单.xml
<Waiters>
  <Waiter enable="在职">
    <name>张青</name>
    <UserName>001</UserName>
    <Password>123</Password>
    <Date>2024-04-29 10:27:40</Date>
    <OnOff>是</OnOff>
  </Waiter>
</Waiters>

## 菜品-x类.xml
<Menu>
  <Dish>
    <Name>香菇青菜</Name>
    <Price>6</Price>
    <OnOff>是</OnOff>
  </Dish>
</Menu>

## 账单.xml
<Bills>
  <Bill Time="2020-1-1" Waiter="张青">
    <Dish Price="12" Num="2">红烧肉</Dish>
    <Dish Price="8" Num="1">香菇青菜</Dish>
    <Dish Price="18" Num="1">鱼头豆腐</Dish>
  </Bill>
</Bills>
