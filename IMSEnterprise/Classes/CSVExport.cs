using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IMSEnterprise
{
    static class CSVString
    {

        private static group findExtensionGroup(List<group> groups)
        {
            foreach(group group in groups)
            {
                if (group.extension != null)
                    if (group.extension.Any != null)
                        if (group.extension.Any.Length != 0)
                            return group;
            }

            return null;
        }

        public static String Groups(ref enterprise ep, ProgressBar progressbar = null)
        {
            try
            {

                StringBuilder all = new StringBuilder("SourcedID,DescriptionShort,DescriptionLong,GroupType,TimeFrameBegin,TimeFrameEnd,CourseCode,Points,SubjectCode,code,point,hours,grade,governedby,municipality_code,municipality_name,phone,code,street,locality,web,area_name,area_code,manager_area");
                all.Append("\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.Count - 1;
                    progressbar.Invoke(new Action(() => progressbar.Maximum = count));
                    progressbar.Invoke(new Action(() => progressbar.Value = 0));
                }

                foreach (group group in ep.group)
                {
                    if (progressbar != null)
                        if (progressbar.Maximum > progressbar.Value)
                            progressbar.Invoke(new Action(() => progressbar.Value++));

                    all.Append(Group(group));
                }

                return all.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String Group(group group)
        {
            try
            {
                String groupCSV = group.sourcedid[0].id + ",";
                if (group.description != null)
                {
                    groupCSV += group.description.@short;
                    groupCSV += "," + group.description.@long + ",";
                }
                else
                    groupCSV += " , ,";
                if (group.grouptype != null && group.grouptype[0] != null)
                {
                    if (group.grouptype[0].typevalue != null && group.grouptype[0].typevalue[0] != null)
                        groupCSV += group.grouptype[0].typevalue[0].Value + ",";
                    else
                        groupCSV += " ,";
                }
                else
                    groupCSV += " ,";
                if (group.timeframe != null)
                {
                    if (group.timeframe.begin != null)
                        groupCSV += group.timeframe.begin.Value + ",";
                    else
                        groupCSV += " ,";
                    if (group.timeframe.end != null)
                        groupCSV += group.timeframe.end.Value + ",";
                    else
                        groupCSV += " ,";
                }
                else
                    groupCSV += " , ,";
                if (group.extension != null)
                {
                    if (group.extension.Any != null)
                    {
                        foreach (XmlNode node in group.extension.Any[0].ChildNodes)
                        {
                            if (node.Name == "#whitespace")
                                groupCSV += ", " + Regex.Replace(node.InnerText, @"\t|\n|\r", "").Trim();
                            else
                                groupCSV += "," + node.InnerText;
                        }
                    }
                }

                return groupCSV + "\r\n";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String Persons(ref enterprise ep, ProgressBar progressbar = null)
        {
            try
            {
                StringBuilder all = new StringBuilder("SourcedID,userID1,userID2,FullName,NameGiven,NameFamily,Email,Tel1,Tel2,Tel3,PostCode,Street,Locailty,Birthday,Gender,InstitutionRoleType,SystemRoleType");

                foreach (XmlNode node in ep.person[0].extension.Any[0].ChildNodes)
                {
                    if (node.Name != "#whitespace")
                        all.Append("," + node.Name);
                }
                all.Append("\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.Count - 1;
                    progressbar.Invoke(new Action(() => progressbar.Maximum = count));
                    progressbar.Invoke(new Action(() => progressbar.Value = 0));
                }

                foreach (person person in ep.person)
                {
                    if (progressbar != null)
                        if (progressbar.Maximum > progressbar.Value)
                            progressbar.Invoke(new Action(() => progressbar.Value++));

                    all.Append(Person(person));
                }

                return all.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String Person(person person)
        {
            try
            {
                String personCSV = person.sourcedid[0].id + ",";
                if (person.userid != null && person.userid[0] != null)
                {
                    if (person.userid[0].Value != null)
                        personCSV += person.userid[0].Value + ",";
                    else
                        personCSV += " ,";
                    if (person.userid[1].Value != null)
                        personCSV += person.userid[1].Value + ",";
                    else
                        personCSV += " ,";

                }
                else
                    personCSV += " , ,";

                if (person.name != null)
                {
                    if (person.name.fn != null)
                        personCSV += person.name.fn + ",";
                    else
                        personCSV += " ,";
                    if (person.name.n != null)
                        if (person.name.n.given != null)
                            personCSV += person.name.n.given + ",";
                        else
                            personCSV += " ,";
                    else
                        personCSV += " ,";
                    if (person.name.n != null)
                        if (person.name.n.family != null)
                            personCSV += person.name.n.family + ",";
                        else
                            personCSV += " ,";
                    else
                        personCSV += " ,";
                }
                else
                    personCSV += " , , ,";
                if (person.email != null)
                    personCSV += person.email + ",";
                else
                    personCSV += " ,";
                if (person.tel != null)
                {
                    for (int i = 0; i < person.tel.Length; i++)
                    {
                        if (person.tel[i] != null)
                            if (person.tel[i].Value != null)
                                personCSV += person.tel[0].Value + ",";
                            else
                                personCSV += " ,";
                        else
                            personCSV += " ,";

                    }
                    personCSV += " ,";
                }
                else
                    personCSV += " ,";
                if (person.adr != null)
                {
                    if (person.adr.pcode != null)
                        personCSV += person.adr.pcode + ",";
                    else
                        personCSV += " ,";
                    if (person.adr.street != null)
                        personCSV += person.adr.street[0] + ",";
                    else
                        personCSV += " ,";
                    if (person.adr.locality != null)
                        personCSV += person.adr.locality + ",";
                    else
                        personCSV += " ,";
                }
                else
                    personCSV += " , , ,";
                if (person.demographics != null)
                {
                    if (person.demographics.bday != null)
                        personCSV += person.demographics.bday + ",";
                    else
                        personCSV += " ,";
                    if (person.demographics.gender != null)
                        personCSV += person.demographics.gender + ",";
                    else
                        personCSV += " ,";
                }
                else
                    personCSV += " , ,";
                if (person.institutionrole != null)
                    if (person.institutionrole[0] != null)
                        personCSV += person.institutionrole[0].institutionroletype.ToString() + ",";
                    else
                        personCSV += " ,";
                else
                    personCSV += " ,";
                if (person.systemrole != null)
                    personCSV += person.systemrole.systemroletype.ToString();
                else
                    personCSV += " ";

                foreach (XmlNode node in person.extension.Any[0].ChildNodes)
                {
                    if (node.Name != "#whitespace")
                        personCSV += "," + Regex.Replace(node.InnerText, @"\t|\n|\r", "");
                }

                return personCSV + "\r\n";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static String getPID(person person)
        {
            try
            {
                foreach (userid uid in person.userid)
                {
                    if (uid.useridtype == "PID")
                    {
                        return uid.Value;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String Memberships(ref enterprise ep, ProgressBar progressbar = null)
        {
            try
            {
                StringBuilder all = new StringBuilder("MSSourcedID,MSourcedid,MIDType,MRoleStatus,MRoleRoleType,MRoleTimeFrameBegin,MRoleTimeFrameEnd\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.Count - 1;
                    progressbar.Invoke(new Action(() => progressbar.Maximum = count));
                    progressbar.Invoke(new Action(() => progressbar.Value = 0));
                }

                foreach (membership membership in ep.membership)
                {
                    if (progressbar != null)
                        if (progressbar.Maximum > progressbar.Value)
                            progressbar.Invoke(new Action(() => progressbar.Value++));

                    all.Append(Membership(membership));
                }

                return all.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String Membership(membership membership)
        {
            try
            {
                StringBuilder membershipCSV = new StringBuilder("");
                foreach (member member in membership.member)
                {
                    membershipCSV.Append(membership.sourcedid.id + "," + member.sourcedid.id + ",");
                    if (member.idtype != null)
                        membershipCSV.Append(member.idtype);
                    else
                        membershipCSV.Append(" ");
                    membershipCSV.Append(",");
                    if (member.role[0] != null)
                    {
                        membershipCSV.Append(member.role[0].status + "," + member.role[0].roletype + ",");
                        if (member.role[0].timeframe != null)
                            membershipCSV.Append(member.role[0].timeframe.begin + "," + member.role[0].timeframe.end + "\r\n");
                        else
                            membershipCSV.Append(" , \r\n");

                    }
                    else
                        membershipCSV.Append(" , , , \r\n");
                }

                return membershipCSV.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
