using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public class ItemThumbnailGeneratorWindow : EditorWindow {

    private int resolution = 512;
    private string outputFolder = "Assets/Generated/ItemThumbnails";
    private bool overwriteExisting = true;

    [MenuItem("Tools/Item Thumbnail Generator")]
    public static void ShowWindow() {
        GetWindow<ItemThumbnailGeneratorWindow>("Item Thumbnails");
    }

    private void OnGUI() {

        GUILayout.Space(10);

        GUILayout.Label("Thumbnail Settings", EditorStyles.boldLabel);

        resolution = EditorGUILayout.IntPopup(
            "Resolution",
            resolution,
            new string[] { "256", "512", "1024" },
            new int[] { 256, 512, 1024 }
        );

        outputFolder = EditorGUILayout.TextField("Output Folder", outputFolder);

        overwriteExisting = EditorGUILayout.Toggle("Overwrite Existing", overwriteExisting);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Thumbnails For Selected ItemData")) {
            GenerateForSelection();
        }
    }

    private void GenerateForSelection() {

        Object[] selection = Selection.objects;

        if (selection == null || selection.Length == 0) {
            Debug.LogWarning("No ItemData selected.");
            return;
        }

        if (!Directory.Exists(outputFolder)) {
            Directory.CreateDirectory(outputFolder);
            AssetDatabase.Refresh();
        }

        foreach (Object obj in selection) {

            ItemData item = obj as ItemData;

            if (item == null) {
                continue;
            }

            if (item.previewPrefab == null) {
                Debug.LogWarning("ItemData missing previewPrefab: " + item.name);
                continue;
            }

            GenerateThumbnail(item, 45f, 30f, false);
            GenerateThumbnail(item, 60f, 30f, true);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Thumbnail generation complete.");
    }

    private void GenerateThumbnail(
        ItemData item,
        float yaw, float pitch,
        bool isForHover
    ) {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(item.previewPrefab);
        instance.hideFlags = HideFlags.HideAndDontSave;

        instance.transform.position = Vector3.zero;
        instance.transform.rotation =
            Quaternion.Euler(0f, 0f, 0f) *
            Quaternion.Euler(item.previewRotation);
        instance.transform.localScale = Vector3.one * item.previewScale;

        Camera camera = CreateCamera();
        camera.hideFlags = HideFlags.HideAndDontSave;

        Bounds bounds = CalculateBounds(instance);

        Vector3 center = bounds.center;
        float radius = bounds.extents.magnitude;

        float camDistance = radius * 2f;

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 camDirection = camRotation * Vector3.back;

        camera.transform.position = center + camDirection * camDistance;
        camera.transform.LookAt(center);

        camera.orthographic = true;
        camera.orthographicSize = radius;

        RenderTexture rt = new RenderTexture(resolution, resolution, 24);
        camera.targetTexture = rt;

        Texture2D tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);

        camera.Render();

        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        
        string prefixName = isForHover? "Item_" : "HoverItem_";
        string filePath = outputFolder+"/"+prefixName + item.name + ".png";

        if (File.Exists(filePath) && !overwriteExisting) {
            Debug.Log("Skipping existing: " + item.name);
        } else {
            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Saved: " + filePath);
        }

        camera.targetTexture = null;
        RenderTexture.active = null;

        DestroyImmediate(instance);
        DestroyImmediate(camera);
        DestroyImmediate(rt);
        DestroyImmediate(tex);

        AssetDatabase.ImportAsset(filePath, ImportAssetOptions.ForceSynchronousImport);

        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(filePath);

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.alphaIsTransparency = true;
        importer.mipmapEnabled = false;
        importer.SaveAndReimport();

        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(filePath);

        if (isForHover) {
            item.hoverIcon = sprite;
        } else {
            item.icon = sprite;
        }
        EditorUtility.SetDirty(item);
    }

    private Camera CreateCamera() {

        GameObject camGO = new GameObject("ThumbnailCamera");
        camGO.hideFlags = HideFlags.HideAndDontSave;
        Camera cam = camGO.AddComponent<Camera>();

        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0f, 0f, 0f, 0f);
        cam.nearClipPlane = 0.01f;
        cam.farClipPlane = 100f;

        return cam;
    }

    private Bounds CalculateBounds(GameObject obj) {

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0) {
            return new Bounds(obj.transform.position, Vector3.one);
        }

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++) {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return bounds;
    }
}
