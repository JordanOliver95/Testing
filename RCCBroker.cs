using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.Linq;
using System.Data.Entity;
namespace RCCJobManagement
{
    public class RCCBroker
    {

        // this is the instance of the database by models inside the RCCDBDATACONTEXT. 
        RCCJobManagement.Models.RCCDBDataContext db = new RCCJobManagement.Models.RCCDBDataContext();

            
            /// <summary>
            /// Creates a new job from the form and all the submitted data
            /// </summary>
            /// <param name="rccId"></param>
            /// <param name="mdsRefId"></param>
            /// <param name="therapyRefId"></param>
            /// <param name="clientId"></param>
            /// <param name="deviceId"></param>
            /// <param name="deliveryMethod"></param>
            /// <param name="statusId"></param>
            /// <param name="reasonId"></param>
            /// <param name="techReqId"></param>
            /// <param name="upholsteryReqId"></param>
            /// <param name="noticeId"></param>
            /// <param name="deviceLocationId"></param>
            /// <param name="admin"></param>
            /// <param name="notes"></param>
            /// <param name="dateReceived"></param>
            /// <param name="dateCheckCompleted"></param>
            /// <param name="deliveryDate"></param>
            /// <param name="totalValue"></param>
            /// <param name="totalHours"></param>
            public void createJob(int rccId, int mdsRefId, int therapyRefId, int clientId, int deviceId, int deliveryMethod, int statusId,
                int reasonId, int techReqId, int upholsteryReqId, int noticeId, int deviceLocationId, string admin, string notes,
                DateTime dateReceived, DateTime dateCheckCompleted, DateTime deliveryDate, double totalValue, double totalHours)
            {
                try
                {

                    RCCJobManagement.Models.Job newJob = new RCCJobManagement.Models.Job();

                newJob.RCCID = rccId;
                newJob.MDSRefID = mdsRefId;
                newJob.ClientID = clientId;
                newJob.DeviceID = deviceId;
                newJob.DeliveryMethodID = deliveryMethod;
                newJob.StatusID = statusId;
                newJob.StatusReasonID = reasonId;
                newJob.TechReqID = techReqId;
                newJob.UphReqID = upholsteryReqId;
                newJob.CompletionNoticeID = noticeId;
                newJob.DeviceLocationID = deviceLocationId;
                newJob.Adm = admin;
                newJob.Notes = notes;
                newJob.ReceivedDate = dateReceived;
                newJob.DateCheckCom = dateCheckCompleted;
                newJob.DeliveryDate = deliveryDate;


                db.Jobs.Add(newJob);

                    updateData();

                }
                catch (Exception e)
                {

                }
            }

            /// <summary>
            /// Method that updates the actual database
            /// </summary>
            private  bool updateData()
            {
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception("Can't save database changes", e);
                    return false;
                }
            }

            /// <summary>
            /// Returns a Job b
            /// </summary>
            /// <param name="primaryKey"></param>
            /// <returns></returns>
            public RCCJobManagement.Models.Job selectJob(int primaryKey)
            {
            
                try
                {
                    return (from Jobs in db.Jobs
                            where Jobs.JobID == primaryKey
                            select Jobs).SingleOrDefault();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            /// <summary>
            /// Updates the old job with the new data that was inputted
            /// </summary>
            /// <param name="pKey"></param>
            /// <param name="rccId"></param>
            /// <param name="mdsRefId"></param>
            /// <param name="therapyRefId"></param>
            /// <param name="clientId"></param>
            /// <param name="deviceId"></param>
            /// <param name="deliveryMethod"></param>
            /// <param name="statusId"></param>
            /// <param name="reasonId"></param>
            /// <param name="techReqId"></param>
            /// <param name="upholsteryReqId"></param>
            /// <param name="noticeId"></param>
            /// <param name="deviceLocationId"></param>
            /// <param name="admin"></param>
            /// <param name="notes"></param>
            /// <param name="dateReceived"></param>
            /// <param name="dateCheckCompleted"></param>
            /// <param name="deliveryDate"></param>
            /// <param name="totalValue"></param>
            /// <param name="totalHours"></param>
            public bool updateJob(int pKey, int rccId, int mdsRefId, int clientId, int deviceId, int deliveryMethod, int statusId,
                int reasonId, int techReqId, int upholsteryReqId, int noticeId, int deviceLocationId, string admin, string notes,
                DateTime dateReceived, DateTime dateCheckCompleted, DateTime deliveryDate)
            {
                try
                {
                    RCCJobManagement.Models.Job newJob = selectJob(pKey);


                    newJob.RCCID = rccId;
                    newJob.MDSRefID = mdsRefId;
                    newJob.ClientID = clientId;
                    newJob.DeviceID = deviceId;
                    newJob.DeliveryMethodID = deliveryMethod;
                    newJob.StatusID = statusId;
                    newJob.StatusReasonID = reasonId;
                    newJob.TechReqID = techReqId;
                    newJob.UphReqID = upholsteryReqId;
                    newJob.CompletionNoticeID = noticeId;
                    newJob.DeviceLocationID = deviceLocationId;
                    newJob.Adm = admin;
                    newJob.Notes = notes;
                    newJob.ReceivedDate = dateReceived;
                    newJob.DateCheckCom = dateCheckCompleted;
                    newJob.DeliveryDate = deliveryDate;

                    updateData();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            /// <summary>
            /// Deletes the job based off the inputted primary key.
            /// </summary>
            /// <param name="pKey"></param>
            /// <returns></returns>
            public bool deleteJob(int pKey)
            {
                try
                {
                    RCCJobManagement.Models.Job job;
                    job = (from Jobs in db.Jobs
                           where Jobs.JobID == pKey
                           select Jobs).Single();
                    db.Jobs.Remove(job);
                    updateData();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            /// <summary>
            /// Used to create a row in the given table, this method is only used on tables that have a primary key and description columns.
            /// 
            /// </summary>
            /// <param name="tableName">Table Name</param>
            /// <returns></returns>
            public bool createRow(string tableName, string description, string code)
            {
                try
                {
                db.DDLContents.Select(x => x.DDLName);
                    int key = db.DDLNames.Where(x => x.Name == tableName).Select(x => x.DDLNameID).SingleOrDefault();
                Models.DDLContent newDropDown = new Models.DDLContent();
                    newDropDown.Description = description;
                    newDropDown.Code = code;
                    newDropDown.DDLNameID = key;
                    db.DDLContents.Add(newDropDown);
                    //This switch will eventually be filled with model names 
                    updateData();
                    //After the switch we will add the row to the table.
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            /// <summary>
            /// This method is used to delete a row from the context menu right click on the datagridview.
            /// </summary>
            /// <param name="tableName"></param>
            /// <param name="pkey"></param>
            /// <returns></returns>
            public bool deleteRow(int pkey)
            {
                try
                {
                Models.DDLContent d = db.DDLContents.Where(x => x.DDLContentID == pkey).SingleOrDefault();
                    db.DDLContents.Remove(d);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            /// <summary>
            /// Used to update a certain row in the current selected table.
            /// </summary>
            /// <param name="tableName"></param>
            /// <param name="pkey"></param>
            /// <returns></returns>
            public bool updateRow(string description, string code, int pkey)
            {
                try
                {
                    Models.DDLContent d = db.DDLContents.Where(x => x.DDLContentID == pkey).SingleOrDefault();
                    d.Description = description;
                    d.Code = code;
                    updateData();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            
            

            /// <summary>
            /// Method used to get the names of all the drop down list tables
            /// </summary>
            /// <returns></returns>
            public IEnumerable<RCCJobManagement.Models.DDLName> tables()
            {
            IEnumerable<RCCJobManagement.Models.DDLName> tables = db.DDLNames.ToList();

            //tables = (from DDLNames in db select Name).toList();
            /*
                String[] tables = new String[12];
                tables[0] = "Completion Notice";
                tables[1] = "Delivery Method";
                tables[2] = "Device";
                tables[3] = "Device Location";
                tables[4] = "External Order Num";
                tables[5] = "External Reference";
                tables[6] = "MSD Reference";
                tables[7] = "Recycled Device Used";
                tables[8] = "Status";
                tables[9] = "Status Reason";
                tables[10] = "Technician Requirement";
                tables[11] = "Upholstery Requirement";
                */
            return tables;
            }

        public IEnumerable<RCCJobManagement.Models.DDLContent> tableContent(int pKey)
        {
            IEnumerable<RCCJobManagement.Models.DDLContent> records = db.DDLContents.Where(x => x.DDLNameID == pKey);

            return records;
        }


        #region EmployeeRoleUsers CRUD

        // This is a region that will mainly focus on the code responsible for the CRUD functionality of the Employee, Roles and Users table, mainly for 
        // the admin manage credentials form. Since this part of the project can be completely isolated without much dependencies with other parts, 
        // it is for the best to keep this hidden to reduce code confusion.

        // start with the Role since its the simplest.

            /// <summary>
            /// This is the create New Role method that will enable the C part of the CRUD functionality into the roles table. This will be used 
            /// by the credential management system used by admins in RCC. will be a simple create that uses the models.
            /// </summary>
            /// <param name="roleName">The name of the role to be created.</param>
            /// <returns>This returns a boolean of true for a success and false for a fail</returns>
        public bool createNewRole(string roleName)
        {
            try
            {
                // first try to create a new role.

                // instantiate an instance of the role model from the context class
                Models.Role newRole = new Models.Role();

                // fill it up with the provided parameter
                newRole.Rolename = roleName;

                // now add it to the database.
                db.Roles.Add(newRole);

                // finally update the data in the database to make sure it saves
                updateData();

                // if it reaches this line of code, then that means everything is a success. return a true to tell the program that it worked.
                return true;
            }
            catch
            {
                // if it fails to create a new role, return false to alert the program.
                return false;
            }   
        }

        /// <summary>
        /// This is the select role method that will select and return the role that is requested by specifying the role id. This is the R in 
        /// CRUD functionaliy and will be used quite a lot when existing roles are reqired to be returned.
        /// </summary>
        /// <param name="roleId">This is the specific Id of the role that is requested to be returned.</param>
        /// <returns>This returns an existing record of a role in the database, or a null if it does not succeed.</returns>
        public Models.Role selectRole(int roleId)
        {
            // try first until you succeed
            try
            {
                // first we try to query the database for the existing role determined by the provided primary key
                // we use lamda linq to acquire it.

                Models.Role neededRole = db.Roles.Where(x => x.RoleID == roleId).Single();

                // if that code succeeds, then that means we have the role.
                // return the role
                return neededRole;
            }
            catch
            {
                // if the code reached this stage, something happened. do not return anything.
                return null;
            }
        }

        /// <summary>
        /// This is the editOldRole method that will be the U part of the CRUD functionality. This will update an existing record by replacing the old
        /// rolename with a new one. This will be used in the manage credential form used by admins in RCC.
        /// </summary>
        /// <param name="roleId">The role id is the id that identifies the existing role in the database. Will be needed to select the old role to edit</param>
        /// <param name="newRoleName">This is the new role name that will replace the old role name, a function this method is written to perfom.</param>
        /// <returns>This method will return a boolean value of true if it succeeds in editing a role and a false if not.</returns>
        public bool editOldRole(int roleId, string newRoleName)
        {
            try
            {
                // first we try to select the existing role from the database using the provided role id
                // we will use the select role method to do this.
                Models.Role roleToUpdate = selectRole(roleId);

                // now that we have selected it, modifiy it by setting a new role name
                roleToUpdate.Rolename = newRoleName;

                // now update the database so it saves the changes
                updateData();

                // if the code reaches this point, this means everything worked. report a success
                return true;
            }
            catch
            {
                // if the method fails, report that it did by returning false.
                return false;
            }
        }

        /// <summary>
        /// This is the method that will delete an existing role in the database by specifiying it's primary key. This is the D part of the 
        /// CRUD functionality and will be used in the manage credentials form used by admins.
        /// </summary>
        /// <param name="roleId">The role id is the id of the specific role existing in the database that will be deleted</param>
        /// <returns>It will return a boolean value of true if the deletion process succeeds and a false value if not.</returns>
        public bool deleteRole(int roleId)
        {
            try
            {
                // first find the role to be deleted.
                // we can use the select role method to do this for us
                Models.Role roleToBeDeleted = selectRole(roleId);

                // once selected, delete that instance from the database 
                db.Roles.Remove(roleToBeDeleted);

                // update the database to save changes
                updateData();

                // if the code reached this, then it means everything ran well. return a true to verify
                return true;
            }
            catch
            {
                // if the code reached this part, then something wrong happened up top. the delete function didnt work so we should notify the 
                // program
                return false;
            }
        }

        // now code the select employee
        // *note* while there are no requirements for the creation, modification and deletion of employees, The create, update and delete functionlity
        // might still be included at some point. For now, only the Read part of CRUD will be implemented for the employees.
        
            /// <summary>
            /// This is the select employee method created as a READ part of CRUD Functionality. This will select and return an existing employee from
            /// the database using the provided employee id. This will only return one employee, and if it cannot, will return a null value.
            /// </summary>
            /// <param name="employeeId">This is the primary key that identifies a unique employee in the database. Needed so that
            /// the proper employee will be returned</param>
            /// <returns>It will return an employee object that will contain all it's information or a null value on failure.</returns>
        public Models.Employee selectEmployee(int employeeId)
        {
            try
            {
                // first try to query the employee from the database using the provided employee id
                Models.Employee employeeToBeReturned = db.Employees.Where(x => x.EmployeeID == employeeId).Single();

                // once queried, and it works, then return that employee.
                return employeeToBeReturned;
            }
            catch
            {
                // if it fails, return null to signify failure
                return null;
            }
        }
        
        
        // now code the users CRUD functionality.
        
        /// <summary>
        /// This is the select user method that will select a specific existing user in the database. This is the R part of CRUD functionality. 
        /// This method will return the user selected by query from the database, or a NULL value if it failed.
        /// </summary>
        /// <param name="userId">This is the user id of the user that is being selected. It is required for the method to do it's job.</param>
        /// <returns>This method will return a user object that can be further used to manipulate the user data, or a NULL value on failure.</returns>
        public Models.User selectUser(int userId)
        {
            try
            {
                // first try if we can query the user from the database
                Models.User userToBeReturned = db.Users.Where(x => x.UserID == userId).Single();

                // if it worked, then return the user object
                return userToBeReturned;
            }
            catch
            {
                // if something wrong happened, code will pop up here. Return a null value to alert the program
                return null;
            }
        }

        /// <summary>
        /// This is the create new user method that is responsible for the C part of CRUD functionality. This will create a new user object
        /// using the models available and add that model object filled with the data needed into the database. 
        /// </summary>
        /// <param name="userName">This is the username of the user that will be used when logging in. Part of an employee's log in credentials</param>
        /// <param name="password">This is a 64 bit password hash that will be stored in the database. Part of an employee's log in credentials.</param>
        /// <param name="roleId">This is the role id assigned to the user. Role id's will determine what priviledges and forms the employee has access to.</param>
        /// <param name="employeeId">This is the id of the employee that uses this user account. </param>
        /// <returns>This will return a boolean indicating if the create functionality passed or fail.</returns>
        public bool createNewUser(string userName, string password, int roleId, int employeeId)
        {
            try
            {
                // first try if we could create the user
                // create a fresh instantiation of a user object to fill up.
                Models.User newUser = new Models.User();

                // assign the provided values to the new user
                newUser.Username = userName;
                newUser.Password = password;
                newUser.RoleID = roleId;
                newUser.EmployeeID = employeeId;

                // now add this new user to the database
                db.Users.Add(newUser);

                // and update the database to save the data.
                updateData();

                // now if all of this has run and reached this point, then it is safe to say that it worked.
                return true;
            }
            catch
            {
                // if the code reached this point, that means something bad has happened up top.
                return false;
            }
        }
        
        /// <summary>
        /// This is the U part of CRUD functionality where we update the old user data from the database using this method.
        /// The requirements never mentioned much but i do know that roles are the definite requirement when updating users. I 
        /// included the password here so that admins can reset passwords when people forget them, and i didnt include the others where they wont
        /// make sense like the employee id changing, of the username since it isnt explicitly required.
        /// </summary>
        /// <param name="userId">The user id of the user in the database, needed to determine which user will be modified.</param>
        /// <param name="password">This is the 64 bit password field, will be replaced by a new one if the current one is forgotten or stolen.</param>
        /// <param name="roleId">This is the role id of the user, and an admin should be able to change the other user's roles, including to the block role.</param>
        /// <returns>this will return a boolean value of true for a success and a false when it has failed.</returns>
        public bool updateOldUser(int userId, string password, int roleId)
        {
            try
            {
                // first try to get the user from the database
                // our select user method should take care of it for us
                Models.User userToUpdate = selectUser(userId);

                // now that we have it (assuming), modify the current values with the provided new ones
                userToUpdate.Password = password;
                userToUpdate.RoleID = roleId;

                // once done, we can update the data on the database to save changes
                updateData();

                // if code has reached this point, we have succeeded in updating the provided user.
                return true;
            }
            catch
            {
                // if an error or something else has happened to stop the update process, return a false
                return false;
            }
        }
        
        /// <summary>
        /// This is the delete user method that will delete the specified user from the database using the provided user Id. This forms the 
        /// D part of the CRUD functionality and will be used by admins who can access the manage user credentials form.
        /// </summary>
        /// <param name="userId">This is the user id of the user account to be deleted.</param>
        /// <returns>It will return a boolean that will indicate true if the delete worked and false if it has not.</returns>
        public bool deleteUser(int userId)
        {
            try
            {
                // try first if we can query the user to be deleted
                // we can use the select user method to do it for us
                Models.User userToBeDeleted = selectUser(userId);

                // then we can delete it.
                db.Users.Remove(userToBeDeleted);

                // update the database to save the changes.
                updateData();

                // then we can return true to indicate a success
                return true;
            }
            catch
            {
                // if something happened that stopped the delete process, indicate a failure by return false
                return false;
            }
        }

        #endregion 

    }

}

    
