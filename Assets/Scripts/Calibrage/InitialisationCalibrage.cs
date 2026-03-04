using TMPro;
using UnityEngine;

public class InitialisationCalibrage : MonoBehaviour
{
    [Header("References")]
    public Transform rootJoueur;
    public Transform pointVirtuelA;
    public Transform pointVirtuelB;

    [Header("Snap Manuel")]
    public float vitesseDeplacement = 1.5f;
    public float snapDistance = 0.25f;

    [Header("Texte de hauteur")]
    public TextMeshProUGUI hauteurJoueurText;

    [Header("Textes de calibrage")]
    public TextMeshProUGUI etatCalibrage;
    public string textEtatCalibrageA;
    public string textEtatCalibrageB;
    public string textEtatCalibrageComplete;

    [Header("Canvas Groups")]
    public GameObject calibrageCanvasGr;
    public GameObject hauteurCanvasGr;
    public GameObject canvasGrGlobal;

    [Header("ReferencePourHauteur")]
    public Transform centerEyeAnchor;
    public float hauteurJoueur;
    public float hauteurAssis;
    public float seuilHauteur;

    Vector3 pointReelA;
    Vector3 pointReelB;

    bool pointASet = false;
    bool pointBSet = false;

    int pointSet = 0;

    public bool calibrageEnCours;
    public bool hauteurEnCours = false;

    public CalibrageData resultatCalibrage;

    private void Start()
    {
        etatCalibrage.text = textEtatCalibrageA;
        calibrageEnCours = true;
    }

    private void Update()
    {
        if (calibrageEnCours)
        {
            // 🎮 Déplacement manuel
            Vector2 moveInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
            move = Quaternion.Euler(0, rootJoueur.eulerAngles.y, 0) * move;

            rootJoueur.position += move * vitesseDeplacement * Time.deltaTime;

            // 🧲 Snap uniquement selon l'étape
            if (pointSet == 0 || pointSet == 1)
                SnapToVirtualWall(pointVirtuelA);

            if (pointSet == 2)
                SnapToVirtualWall(pointVirtuelB);

            // 🎯 Capture bouton A
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                pointSet++;

                if (pointSet > 3) pointSet = 0;

                switch (pointSet)
                {
                    case 0:
                        RefaireCalibrage();
                        break;

                    case 1:
                        CapturePointA();
                        break;

                    case 2:
                        CapturePointB();
                        Calibrer();
                        break;
                }
            }

            // 🅱 Quitter calibrage
            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {
                calibrageEnCours = false;
            }
        }

        if (hauteurEnCours)
        {
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                GetJoueurHauteur();
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {
                hauteurEnCours = false;
            }
        }
    }

    // ---------------- SNAP ----------------

    void SnapToVirtualWall(Transform pointVirtuel)
    {
        if (pointVirtuel == null)
            return;

        Vector3 toWall = rootJoueur.position - pointVirtuel.position;

        // Ignorer la hauteur
        toWall.y = 0f;

        float distance = toWall.magnitude;

        if (distance < snapDistance)
        {
            Vector3 correction = -toWall.normalized * distance;
            rootJoueur.position += correction;
        }
    }

    // --- CAPTURE POINT A ---
    public void CapturePointA()
    {
        pointReelA = rootJoueur.position;
        pointASet = true;

        etatCalibrage.text = textEtatCalibrageB;

        Debug.Log("Point A capturé (mur d'entrée)");
    }

    // --- CAPTURE POINT B ---
    public void CapturePointB()
    {
        if (!pointASet) return;

        pointReelB = rootJoueur.position;
        pointBSet = true;

        etatCalibrage.text = textEtatCalibrageComplete;

        Debug.Log("Point B capturé (mur gauche)");
    }

    // --- RESET CALIBRAGE ---
    public void RefaireCalibrage()
    {
        pointASet = false;
        pointBSet = false;
        pointSet = 0;

        etatCalibrage.text = textEtatCalibrageA;

        rootJoueur.localPosition = Vector3.zero;
        rootJoueur.localRotation = Quaternion.identity;

        Debug.Log("Réinitialisation du calibrage");
    }

    // --- CALIBRAGE COMPLET ---
    void Calibrer()
    {
        if (!pointASet || !pointBSet) return;

        Vector3 realDir = pointReelB - pointReelA;
        Vector3 virtualDir = pointVirtuelB.position - pointVirtuelA.position;

        realDir.y = 0f;
        virtualDir.y = 0f;

        realDir.Normalize();
        virtualDir.Normalize();

        float angle = Vector3.SignedAngle(realDir, virtualDir, Vector3.up);
        Quaternion rotationOffset = Quaternion.Euler(0f, angle, 0f);

        rootJoueur.rotation = rotationOffset * rootJoueur.rotation;

        Vector3 newRealA = rotationOffset * (pointReelA - rootJoueur.position) + rootJoueur.position;

        Vector3 positionOffset = pointVirtuelA.position - newRealA;
        rootJoueur.position += positionOffset;

        resultatCalibrage = new CalibrageData
        {
            rotationOffset = rotationOffset,
            positionOffset = positionOffset
        };

        SessionData.calibrage = resultatCalibrage;

        Debug.Log("Calibration terminée ✔");
    }

    // --- CALCUL HAUTEUR ---
    public void GetJoueurHauteur()
    {
        hauteurJoueur = centerEyeAnchor.position.y;

        if (hauteurJoueur <= 0f)
            hauteurJoueur = 1.6f;

        hauteurAssis = hauteurJoueur - seuilHauteur;

        string valeurHauteurString = string.Format("{0:F2}", hauteurJoueur);

        hauteurJoueurText.text = "Ta hauteur approximative est de " + valeurHauteurString + " mètre";

        SessionData.hauteurJoueur = hauteurJoueur;
        SessionData.hauteurAssis = hauteurAssis;
    }

    public void ConfirmerCalibrage()
    {
        UIManager.Instance.HideUI(calibrageCanvasGr);
        UIManager.Instance.ShowUIDelay(hauteurCanvasGr);
    }

    public void ConfirmerHauteur()
    {
        UIManager.Instance.HideUI(canvasGrGlobal);
        GameManager.Instance.ChargerScene(2);
    }
}