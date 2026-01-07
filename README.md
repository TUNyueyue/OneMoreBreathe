These has a fatal bug still,at the script named by "Craft":
When use WeaponFactory to get a weapon,you pass a "data" which is a reference type,it's just like pointer in C.
Therefore,you'll find your data field,which provide to unity inspector,will be changed by your action,such as use weapon and reduce it's durability.
To solve this problem,you can new a data at the top of method,it has a different reference from the data field.
