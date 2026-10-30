#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Creates a playable greybox facility and placeholder Tux.
/// Run from Tools > TuxHorror > Create Starter Scene.
/// </summary>
public static class TuxHorrorSceneBuilder
{
    private const string ScenePath = "Assets/Scenes/TuxHorror_Prototype.unity";

    [MenuItem("Tools/TuxHorror/Create Starter Scene")]
    public static void CreateStarterScene()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
            AssetDatabase.CreateFolder("Assets", "Scenes");

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        Material floorMat = MakeMaterial("Facility_Floor", new Color(0.16f, 0.18f, 0.2f));
        Material wallMat = MakeMaterial("Facility_Walls", new Color(0.25f, 0.28f, 0.31f));
        Material trimMat = MakeMaterial("Facility_Trim", new Color(0.08f, 0.1f, 0.12f));
        Material lampMat = MakeMaterial("Facility_Light", new Color(0.75f, 0.83f, 0.9f));
        Material tuxBlack = MakeMaterial("Tux_Black", new Color(0.025f, 0.035f, 0.05f));
        Material tuxWhite = MakeMaterial("Tux_White", new Color(0.92f, 0.93f, 0.9f));
        Material tuxOrange = MakeMaterial("Tux_Orange", new Color(1f, 0.38f, 0.04f));
        Material eyeMat = MakeMaterial("Tux_Eyes", Color.black);

        MakeBlock("Facility Floor", new Vector3(0f, -0.5f, 0f), new Vector3(12f, 1f, 60f), floorMat);
        MakeBlock("Left Wall", new Vector3(-6f, 2.5f, 0f), new Vector3(0.5f, 5f, 60f), wallMat);
        MakeBlock("Right Wall", new Vector3(6f, 2.5f, 0f), new Vector3(0.5f, 5f, 60f), wallMat);
        MakeBlock("Far Wall", new Vector3(0f, 2.5f, 30f), new Vector3(12f, 5f, 0.5f), wallMat);

        for (int i = -2; i <= 2; i++)
        {
            float z = i * 10f;
            MakeBlock("Ceiling Beam", new Vector3(0f, 5.1f, z), new Vector3(12f, 0.25f, 0.35f), trimMat);
            GameObject lamp = MakeBlock("Ceiling Light", new Vector3(0f, 4.88f, z - 2f),
                new Vector3(2.2f, 0.08f, 0.5f), lampMat);
            Light point = new GameObject("Facility Light").AddComponent<Light>();
            point.type = LightType.Point;
            point.color = new Color(0.72f, 0.82f, 1f);
            point.intensity = 5f;
            point.range = 12f;
            point.transform.position = lamp.transform.position + Vector3.down * 0.4f;
        }

        MakeBlock("Storage Crate A", new Vector3(-3.8f, 0.8f, 7f), new Vector3(1.4f, 1.6f, 1.4f), trimMat);
        MakeBlock("Storage Crate B", new Vector3(3.7f, 0.6f, -9f), new Vector3(1.2f, 1.2f, 1.2f), trimMat);
        MakeBlock("Research Console", new Vector3(4.7f, 1.1f, 16f), new Vector3(1.1f, 2.2f, 2.4f), trimMat);

        GameObject tux = new GameObject("Tux");
        tux.transform.position = new Vector3(0f, 0.02f, -20f);
        CharacterController cc = tux.AddComponent<CharacterController>();
        cc.height = 2f;
        cc.radius = 0.42f;
        cc.center = new Vector3(0f, 1f, 0f);
        cc.stepOffset = 0.3f;
        cc.slopeLimit = 45f;
        MakeTuxVisuals(tux.transform, tuxBlack, tuxWhite, tuxOrange, eyeMat);
        TuxThirdPersonController movement = tux.AddComponent<TuxThirdPersonController>();

        GameObject cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.fieldOfView = 68f;
        camera.farClipPlane = 150f;
        cameraObject.AddComponent<AudioListener>();
        TuxThirdPersonCamera cameraRig = cameraObject.AddComponent<TuxThirdPersonCamera>();
        cameraRig.SetTarget(tux.transform);
        movement.SetCamera(cameraObject.transform);

        GameObject ambient = new GameObject("Facility Ambient Light");
        Light directional = ambient.AddComponent<Light>();
        directional.type = LightType.Directional;
        directional.intensity = 0.22f;
        directional.color = new Color(0.65f, 0.72f, 0.85f);
        ambient.transform.rotation = Quaternion.Euler(45f, -30f, 0f);

        RenderSettings.ambientLight = new Color(0.12f, 0.14f, 0.18f);
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.12f, 0.15f, 0.19f);
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.018f;

        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[]
        {
            new EditorBuildSettingsScene(ScenePath, true)
        };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeGameObject = tux;
        Debug.Log("TuxHorror prototype scene created at " + ScenePath);
        if (!Application.isBatchMode)
        {
            EditorUtility.DisplayDialog("TuxHorror",
                "Starter scene created! Open Assets/Scenes/TuxHorror_Prototype and press Play.",
                "Let's go!");
        }
    }

    private static void MakeTuxVisuals(Transform parent, Material black, Material white,
        Material orange, Material eyes)
    {
        MakeSphere("Body", parent, new Vector3(0f, 1.02f, 0f), new Vector3(0.82f, 1.12f, 0.62f), black);
        MakeSphere("Belly", parent, new Vector3(0f, 0.98f, 0.43f), new Vector3(0.61f, 0.88f, 0.22f), white);
        MakeSphere("Head", parent, new Vector3(0f, 1.88f, 0.02f), new Vector3(0.78f, 0.72f, 0.7f), black);
        MakeSphere("Left Flipper", parent, new Vector3(-0.53f, 1.05f, 0f),
            new Vector3(0.25f, 0.75f, 0.34f), black).transform.localRotation = Quaternion.Euler(0f, 0f, -18f);
        MakeSphere("Right Flipper", parent, new Vector3(0.53f, 1.05f, 0f),
            new Vector3(0.25f, 0.75f, 0.34f), black).transform.localRotation = Quaternion.Euler(0f, 0f, 18f);
        MakeSphere("Beak", parent, new Vector3(0f, 1.82f, 0.65f), new Vector3(0.28f, 0.16f, 0.34f), orange);
        MakeSphere("Left Eye", parent, new Vector3(-0.2f, 2.02f, 0.59f), new Vector3(0.09f, 0.11f, 0.055f), eyes);
        MakeSphere("Right Eye", parent, new Vector3(0.2f, 2.02f, 0.59f), new Vector3(0.09f, 0.11f, 0.055f), eyes);
        MakeSphere("Left Foot", parent, new Vector3(-0.25f, 0.16f, 0.2f), new Vector3(0.3f, 0.16f, 0.48f), orange);
        MakeSphere("Right Foot", parent, new Vector3(0.25f, 0.16f, 0.2f), new Vector3(0.3f, 0.16f, 0.48f), orange);
    }

    private static GameObject MakeSphere(string objectName, Transform parent, Vector3 localPosition,
        Vector3 localScale, Material material)
    {
        GameObject part = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        part.name = objectName;
        part.transform.SetParent(parent, false);
        part.transform.localPosition = localPosition;
        part.transform.localScale = localScale;
        part.GetComponent<Renderer>().sharedMaterial = material;
        Object.DestroyImmediate(part.GetComponent<Collider>());
        return part;
    }

    private static GameObject MakeBlock(string objectName, Vector3 position, Vector3 scale, Material material)
    {
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        block.name = objectName;
        block.transform.position = position;
        block.transform.localScale = scale;
        block.GetComponent<Renderer>().sharedMaterial = material;
        return block;
    }

    private static Material MakeMaterial(string materialName, Color color)
    {
        if (!AssetDatabase.IsValidFolder("Assets/Materials"))
            AssetDatabase.CreateFolder("Assets", "Materials");

        string path = "Assets/Materials/" + materialName + ".mat";
        Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null)
            throw new System.InvalidOperationException("No supported shader was found.");

        if (material == null)
        {
            material = new Material(shader) { name = materialName };
            AssetDatabase.CreateAsset(material, path);
        }
        else
        {
            material.shader = shader;
        }

        material.color = color;
        EditorUtility.SetDirty(material);
        return material;
    }
}
#endif
