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
            public bool createRow(int key, string tableName, string description, string code)
            {
                try
                {
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
                updateData();

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
            public bool updateRow(int Contentpkey, string description, string code)
            {
                try
                {
                    Models.DDLContent d = db.DDLContents.Where(x => x.DDLContentID == Contentpkey).SingleOrDefault();
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

        }
    }

    
