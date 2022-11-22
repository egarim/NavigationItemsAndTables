# NavigationItemsAndTables

This project generates a json file with the structure of the navigation items and the tables where each navigation item is pointing


[
  {
    "$id": "1",
    "Name": "Default",
    "ParentNode": null,
    "ParentNodeName": null,
    "Table": null
  },
  {
    "$id": "2",
    "Name": "Users",
    "ParentNode": {
      "$ref": "1"
    },
    "ParentNodeName": "Default",
    "Table": "PermissionPolicyUser"
  },
  {
    "$id": "3",
    "Name": "Domain Object 1",
    "ParentNode": {
      "$ref": "1"
    },
    "ParentNodeName": "Default",
    "Table": "DomainObject1"
  },
  {
    "$id": "4",
    "Name": "Domain Object 2",
    "ParentNode": {
      "$ref": "1"
    },
    "ParentNodeName": "Default",
    "Table": "DomainObject2"
  },
  {
    "$id": "5",
    "Name": "Role",
    "ParentNode": {
      "$ref": "1"
    },
    "ParentNodeName": "Default",
    "Table": "PermissionPolicyRole"
  },
  {
    "$id": "6",
    "Name": "Main",
    "ParentNode": null,
    "ParentNodeName": null,
    "Table": null
  },
  {
    "$id": "7",
    "Name": "Sub1",
    "ParentNode": {
      "$ref": "6"
    },
    "ParentNodeName": "Main",
    "Table": null
  },
  {
    "$id": "8",
    "Name": "Sub2",
    "ParentNode": {
      "$ref": "7"
    },
    "ParentNodeName": "Sub1",
    "Table": null
  },
  {
    "$id": "9",
    "Name": "Domain Object 2",
    "ParentNode": {
      "$ref": "8"
    },
    "ParentNodeName": "Sub2",
    "Table": "DomainObject2"
  }
]