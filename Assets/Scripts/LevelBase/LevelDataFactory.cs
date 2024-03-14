public static class LevelDataFactory
{
    public static LevelData CreateLevelData(int level)
    {
        LevelData levelData;
        switch (level)
        {
            case 1:
                levelData = new LevelData_0();
                break;
            case 2:
                levelData = new LevelData_1();
                break;
            case 3:
                levelData = new LevelData_2();
                break;
            case 4:
                levelData = new LevelData_3();
                break;
            case 5:
                levelData = new LevelData_4();
                break;
            case 6:
                levelData = new LevelData_5();
                break;
            case 7:
                levelData = new LevelData_6();
                break;
            case 8:
                levelData = new LevelData_7();
                break;
            case 9:
                levelData = new LevelData_8();
                break;
            default:
                levelData = new LevelData_0();
                break;
        }
        levelData.Initialize();
        return levelData;
    }
}