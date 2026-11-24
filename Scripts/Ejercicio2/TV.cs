using UnityEngine;

public class TV : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private Material tvMaterial;

    // Variables declaradas para guardar las capturas
    private string savePath;
    private int captureCounter = 1;

    void Start()
    {
        // Ruta donde se guardarán las fotos 
        savePath = Application.dataPath + "/Captura_";

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            tvMaterial = renderer.material;
        }

        // Comprueba si hay cámaras conectadas
        if (WebCamTexture.devices.Length > 0)
        {
            string nombreCamara = WebCamTexture.devices[0].name;
            Debug.Log("Cámara detectada: " + nombreCamara);
            webCamTexture = new WebCamTexture(nombreCamara);
        }
        else
        {
            Debug.Log("[ERROR] Unity no detecta ninguna webcam conectada.");
        }
    }

    void Update()
    {
        // Enciende la webcam con la tecla S
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (webCamTexture != null && !webCamTexture.isPlaying)
            {
                tvMaterial.mainTexture = webCamTexture;
                webCamTexture.Play();
            }
        }

        // Captura un fotograma con la tecla X
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (webCamTexture != null && webCamTexture.isPlaying)
            {
                Texture2D snapshot = new Texture2D(webCamTexture.width, webCamTexture.height);
                snapshot.SetPixels(webCamTexture.GetPixels());
                snapshot.Apply();
                
                string rutaCompleta = savePath + captureCounter.ToString() + ".png";
                System.IO.File.WriteAllBytes(rutaCompleta, snapshot.EncodeToPNG());
                
                Debug.Log("[FOTO GUARDADA] en: " + rutaCompleta);
                captureCounter++;
            }
            else
            {
                Debug.Log("Enciende primero la cámara antes de hacer una captura.");
            }
        }

        // Detiene la cámara con Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (webCamTexture != null && webCamTexture.isPlaying)
            {
                webCamTexture.Stop();
            }
        }
    }
}