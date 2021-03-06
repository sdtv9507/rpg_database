using Godot;
using System;

[Tool]
public class SystemScript : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int editedField = -1;
    // Called when the node enters the scene tree for the first time.
    public void Start()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary statsData = jsonDictionary["stats"] as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary weaponsData = jsonDictionary["weapons"] as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary armorsData = jsonDictionary["armors"] as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary elementsData = jsonDictionary["elements"] as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary slotsData = jsonDictionary["slots"] as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary skillsData;

        if (jsonDictionary.Contains("skills"))
        {
            skillsData = jsonDictionary["skills"] as Godot.Collections.Dictionary;
        }
        else
        {
            skillsData = new Godot.Collections.Dictionary();
            skillsData.Add("0", "Skills");
            skillsData.Add("1", "Magic");
            jsonDictionary["skills"] = skillsData;
            this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
        }

        GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").Clear();
        GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").Clear();
        GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").Clear();
        GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").Clear();
        GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").Clear();
        GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList").Clear();
        GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").Clear();

        for (int i = 0; i < statsData.Count; i++)
        {
            ItemList item = GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList");
            item.AddItem((statsData[i.ToString()]).ToString());
        }

        for (int i = 0; i < weaponsData.Count; i++)
        {
            ItemList item = GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList");
            item.AddItem((weaponsData[i.ToString()]).ToString());
        }

        for (int i = 0; i < armorsData.Count; i++)
        {
            ItemList item = GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList");
            item.AddItem((armorsData[i.ToString()]).ToString());
        }

        for (int i = 0; i < elementsData.Count; i++)
        {
            ItemList item = GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList");
            item.AddItem((elementsData[i.ToString()]).ToString());
        }
        
        for (int i = 0; i < skillsData.Count; i++)
        {
            ItemList item = GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList");
            item.AddItem((skillsData[i.ToString()]).ToString());
        }

        foreach (string id in slotsData.Keys)
        {
            ItemList kind = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList");
            String kindId = id[0].ToString();
            ItemList type = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList");
            switch (kindId)
            {
                case "w":
                    kind.AddItem("Weapon");
                    type.AddItem((slotsData[id]).ToString());
                    break;
                case "a":
                    kind.AddItem("Armor");
                    type.AddItem((slotsData[id]).ToString());
                    break;
            }
        }
    }

    private void SaveData()
    {
        SaveStats();
        SaveWeapons();
        SaveArmors();
        SaveElements();
        SaveSlots();
        SaveSkills();
    }
    private void SaveStats()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary statsData = new Godot.Collections.Dictionary();

        int statSize = GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").GetItemCount();
        for (int i = 0; i < statSize; i++)
        {
            String text = GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").GetItemText(i);
            statsData.Add(i.ToString(), text);
        }
        jsonDictionary["stats"] = statsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }

    private void SaveWeapons()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary weaponsData = new Godot.Collections.Dictionary();

        int weaponSize = GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").GetItemCount();
        for (int i = 0; i < weaponSize; i++)
        {
            String text = GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").GetItemText(i);
            weaponsData.Add(i.ToString(), text);
        }
        jsonDictionary["weapons"] = weaponsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }

    private void SaveArmors()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary armorsData = new Godot.Collections.Dictionary();

        int armorSize = GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").GetItemCount();
        for (int i = 0; i < armorSize; i++)
        {
            String text = GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").GetItemText(i);
            armorsData.Add(i.ToString(), text);
        }
        jsonDictionary["armors"] = armorsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }

    private void SaveElements()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary elementsData = new Godot.Collections.Dictionary();

        int elementSize = GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").GetItemCount();
        for (int i = 0; i < elementSize; i++)
        {
            String text = GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").GetItemText(i);
            elementsData.Add(i.ToString(), text);
        }
        jsonDictionary["elements"] = elementsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }

    private void SaveSkills()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary skillsData = new Godot.Collections.Dictionary();

        int skillSize = GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").GetItemCount();
        for (int i = 0; i < skillSize; i++)
        {
            String text = GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").GetItemText(i);
            skillsData.Add(i.ToString(), text);
        }
        jsonDictionary["skills"] = skillsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }

    private void SaveSlots()
    {
        Godot.Collections.Dictionary jsonDictionary = this.GetParent().GetParent().Call("ReadData", "System") as Godot.Collections.Dictionary;
        Godot.Collections.Dictionary slotsData = new Godot.Collections.Dictionary();

        int slotSize = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").GetItemCount();
        for (int i = 0; i < slotSize; i++)
        {
            String kind = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").GetItemText(i);
            String id = "";
            switch (kind)
            {
                case "Weapon":
                    id = "w";
                    break;
                case "Armor":
                    id = "a";
                    break;
            }
            String text = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList").GetItemText(i);
            id += i.ToString();
            slotsData.Add(id.ToString(), text);
        }
        jsonDictionary["slots"] = slotsData;
        this.GetParent().GetParent().Call("StoreData", "System", jsonDictionary);
    }
    private void _on_OKButton_pressed()
    {
        string name = GetNode<LineEdit>("EditField/FieldName").Text;
        if (name != "")
        {
            if (editedField == 0)
            {
                GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").AddItem(name);
            }
            else if (editedField == 1)
            {
                GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").AddItem(name);
            }
            else if (editedField == 2)
            {
                GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").AddItem(name);
            }
            else if (editedField == 3)
            {
                GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").AddItem(name);
            }
            else if (editedField == 4)
            {
                GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").AddItem(name);
            }
            SaveData();
        }
        GetNode<WindowDialog>("EditField").Hide();
        editedField = -1;
    }

    private void _on_CancelButton_pressed()
    {
        editedField = -1;
        GetNode<WindowDialog>("EditField").Hide();
    }
    private void _on_EditField_popup_hide()
    {
        editedField = -1;
        GetNode<WindowDialog>("EditField").Hide();
    }

    private void _on_AddStat_pressed()
    {
        editedField = 0;
        GetNode<WindowDialog>("EditField").WindowTitle = "Add Stat";
        GetNode<WindowDialog>("EditField").PopupCentered(new Vector2(392, 95));
    }

    private void _on_RemoveStat_pressed()
    {
        int index = GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("StatsLabel/StatsContainer/StatsBoxContainer/StatsList").RemoveItem(index);
            SaveData();
        }
    }

    private void _on_AddWeapon_pressed()
    {
        editedField = 1;
        GetNode<WindowDialog>("EditField").WindowTitle = "Add Weapon";
        GetNode<WindowDialog>("EditField").PopupCentered(new Vector2(392, 95));
    }

    private void _on_RemoveWeapon_pressed()
    {
        int index = GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("WeaponTypesLabel/WeaponTypesContainer/WpBoxContainer/WeaponList").RemoveItem(index);
            SaveData();
        }
    }

    private void _on_AddArmor_pressed()
    {
        editedField = 2;
        GetNode<WindowDialog>("EditField").WindowTitle = "Add Armor";
        GetNode<WindowDialog>("EditField").PopupCentered(new Vector2(392, 95));
    }

    private void _on_RemoveArmor_pressed()
    {
        int index = GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("ArmorTypesLabel/ArmorTypesContainer/ArBoxContainer/ArmorList").RemoveItem(index);
            SaveData();
        }
    }

    private void _on_AddElement_pressed()
    {
        editedField = 3;
        GetNode<WindowDialog>("EditField").WindowTitle = "Add Element";
        GetNode<WindowDialog>("EditField").PopupCentered(new Vector2(392, 95));
    }

    private void _on_RemoveElement_pressed()
    {
        int index = GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("ElementLabel/ElementContainer/EleBoxContainer/ElementList").RemoveItem(index);
            SaveData();
        }
    }

    private void _on_AddSkillType_pressed()
    {
        editedField = 4;
        GetNode<WindowDialog>("EditField").WindowTitle = "Add Skill Type";
        GetNode<WindowDialog>("EditField").PopupCentered(new Vector2(392, 95));
    }

    private void _on_RemoveSKillType_pressed()
    {
        int index = GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("SkillTypesLabel/SkillTypeContainer/VBoxContainer/SkillTypeList").RemoveItem(index);
            SaveData();
        }
    }
    private void _on_AddSet_pressed()
    {
        GetNode<WindowDialog>("AddSlot").PopupCentered();
    }

    private void _on_RemoveSet_pressed()
    {
        int index = GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList").GetSelectedItems()[0];
        if (index > -1)
        {
            GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").RemoveItem(index);
            GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList").RemoveItem(index);
            SaveData();
        }
    }

    private void _on_AddSlotCancelButton_pressed()
    {
        GetNode<WindowDialog>("AddSlot").Hide();
    }

    private void _on_AddSlotOkButton_pressed()
    {
        int kind = GetNode<OptionButton>("AddSlot/TypeLabel/TypeButton").Selected;
        switch (kind)
        {
            case 0:
                GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").AddItem("Weapon");
                break;
            case 1:
                GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/KindList").AddItem("Armor");
                break;
        }
        String name = GetNode<LineEdit>("AddSlot/NameLabel/NameEdit").Text;
        GetNode<ItemList>("EquipmentLabel/EquipContainer/SetContainer/SetDivisor/TypeList").AddItem(name);
        SaveData();
        GetNode<WindowDialog>("AddSlot").Hide();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
