using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GroupWinTitle = "Group editor";
        public static string GroupDeleteWinTitle = "Delete group";
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupsList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupDialogue();

            string count = autx.ControlTreeView(GroupWinTitle, "",
                "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount","#0","");

            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = autx.ControlTreeView(GroupWinTitle, "",
                "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetText", "#0|#"+i, "");

                list.Add(new GroupData
                {
                    Name = item
                });
            }

            CloseGroupDialogue();

            return list;
        }

        internal void Modify(int index, GroupData group)
        {
            OpenGroupDialogue();
            SelectGroup(index);
            autx.ControlClick(GroupWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d52");
            autx.Send(group.Name);
            autx.Send("{ENTER}");
            autx.WinWait(GroupWinTitle);
            CloseGroupDialogue();
        }

        public void Remove(int index)
        {
            OpenGroupDialogue();
            SelectGroup(index);
            autx.ControlClick(GroupWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            autx.WinWait(GroupDeleteWinTitle);
            autx.ControlClick(GroupDeleteWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            autx.WinWait(GroupWinTitle);
            CloseGroupDialogue();
        }

        public List<GroupData> CreateIfNotExists(int index)
        {
            List<GroupData> oldGroups = GetGroupsList();
            if (oldGroups.Count == 1)
            {
                GroupData newGroup = new GroupData()
                {
                    Name = "group_for_tests"
                };
                AddGroup(newGroup);
                oldGroups = GetGroupsList();
            }
            return oldGroups;
        }

        private void SelectGroup(int index)
        {
            autx.ControlTreeView(GroupWinTitle, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", "#0|#" + index, "");
        }

        public void AddGroup(GroupData newGroup)
        {
            OpenGroupDialogue();
            autx.ControlClick(GroupWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            autx.Send(newGroup.Name);
            autx.Send("{ENTER}");
            CloseGroupDialogue();
        }

        private void CloseGroupDialogue()
        {
            autx.ControlClick(GroupWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        private void OpenGroupDialogue()
        {
            autx.ControlClick(winTitle, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            autx.WinWait(GroupWinTitle);
        }
    }
}