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
/*
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
*/
        public static String Groups(ref enterprise ep, ProgressBar progressbar = null)
        {
            try
            {

                var all = new StringBuilder("SourcedID,DescriptionShort,DescriptionLong,GroupType,TimeFrameBegin,TimeFrameEnd,CourseCode,Points,SubjectCode,code,point,hours,grade,governedby,municipality_code,municipality_name,phone,code,street,locality,web,area_name,area_code,manager_area");
                all.Append("\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.ToList().Count - 1;
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
                    if (group.extension.coursecode != null)
                        groupCSV += group.extension.coursecode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.csncode != null)
                        groupCSV += group.extension.csncode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.governedby != null)
                        groupCSV += group.extension.governedby + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.groupusage != null)
                        groupCSV += group.extension.groupusage + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.hours != null)
                        groupCSV += group.extension.hours + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.languagecode != null)
                        groupCSV += group.extension.languagecode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.locality != null)
                        groupCSV += group.extension.locality + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.municipalitycode != null)
                        groupCSV += group.extension.municipalitycode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.municipalityname != null)
                        groupCSV += group.extension.municipalityname + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.pcode != null)
                        groupCSV += group.extension.pcode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.period != null)
                        groupCSV += group.extension.period + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.phone != null)
                        groupCSV += group.extension.phone + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.point != null)
                        groupCSV += group.extension.point + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.schooltype != null)
                        groupCSV += group.extension.schooltype + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.schoolyear != null)
                        groupCSV += group.extension.schoolyear + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.street != null)
                        groupCSV += group.extension.street + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.subjectcode != null)
                        groupCSV += group.extension.subjectcode + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.timestamp != null)
                        groupCSV += group.extension.timestamp + ",";
                    else
                        groupCSV += " ,";
                    if (group.extension.web != null)
                        groupCSV += group.extension.web + ",";
                    else
                        groupCSV += " ,";
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
                var all = new StringBuilder("SourcedID,userID1,userID2,FullName,NameGiven,NameFamily,Email,Tel1,Tel2,Tel3,PostCode,Street,Locailty,Birthday,Gender,InstitutionRoleType,SystemRoleType");

                all.Append(
                    ",AltAdrExtAdd,AltAdrLocality,AltAdrPcode,AltAdrStreet,emailwork,employmentend,employmentstart");
                all.Append(
                    ",municipalitycode,municipalityname,nativelanguage,populationcityadmin,populationcityarea");
                all.Append(
                    ",populationkeycode,privacy,programcode,schoolunitcode,signature,timestamp");

                all.Append("\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.ToList().Count - 1;
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
                    personCSV += " ,";

                if (person.extension.altadr != null)
                {
                    personCSV += person.extension.altadr.extadd + ",";
                    personCSV += person.extension.altadr.locality + ",";
                    personCSV += person.extension.altadr.pcode + ",";
                    personCSV += person.extension.altadr.street + ",";
                }
                else
                    personCSV += " , , , ,";

                if (person.extension.emailwork != null)
                {
                    personCSV += person.extension.emailwork + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.employmentend != null)
                {
                    personCSV += person.extension.employmentend + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.employmentstart != null)
                {
                    personCSV += person.extension.employmentstart + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.municipalitycode != null)
                {
                    personCSV += person.extension.municipalitycode + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.municipalityname != null)
                {
                    personCSV += person.extension.municipalityname + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.nativelanguage != null)
                {
                    personCSV += person.extension.nativelanguage + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.geographickeycode != null)
                {
                    personCSV += person.extension.geographickeycode + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.privacy != null)
                {
                    personCSV += person.extension.privacy + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.programcode != null)
                {
                    personCSV += person.extension.programcode + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.schoolunitcode != null)
                {
                    personCSV += person.extension.schoolunitcode + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.signature != null)
                {
                    personCSV += person.extension.signature + ",";
                }
                else
                    personCSV += " ,";

                if (person.extension.timestamp != null)
                {
                    personCSV += person.extension.timestamp + ",";
                }
                else
                    personCSV += " ,";

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
                var all = new StringBuilder("MSSourcedID,MSourcedid,MIDType,MRoleStatus,MRoleRoleType,MRoleTimeFrameBegin,MRoleTimeFrameEnd\r\n");

                if (progressbar != null)
                {
                    int count = ep.group.ToList().Count - 1;
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
                var membershipCSV = new StringBuilder("");
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
