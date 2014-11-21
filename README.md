Xrm-Ext
=======

Collection of extension methods for Dynamics CRM.

This is a work in progress.

Compare2: Compares two entity records to know if they have different attribute values.

Usage
=======

Compare2:

  var e1 = new Entity("account");
  var e2 = new Entity("account");
  
  var areEqualResult = e1.IsEqualTo(e2); //Will return true if e1 has exactly the same attributes and with the same values
  
  
