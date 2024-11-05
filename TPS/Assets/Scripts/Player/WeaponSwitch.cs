using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] guns; // Gunオブジェクトの配列
    private int currentGunIndex = 0; // 現在アクティブなGunオブジェクトのインデックス

    public float switchCooldown = 1.0f; // 武器切り替えのクールダウン時間（秒）
    private float lastSwitchTime; // 最後に武器を切り替えた時間

    public Image[] weaponIcons; // 武器アイコンのImageコンポーネントの配列

    void Start()
    {
        // 初期状態で最初の銃以外を非アクティブにする
        UpdateWeaponActiveState();
        UpdateWeaponIcons();
    }

    void Update()
    {
        // 右クリックが押されたかを検出し、クールダウンが終了しているか確認する
        if (Input.GetMouseButtonDown(1) && Time.time >= lastSwitchTime + switchCooldown)
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // 現在の銃を非アクティブにする
        guns[currentGunIndex].SetActive(false);

        // 次の銃のインデックスを計算する
        currentGunIndex = (currentGunIndex + 1) % guns.Length; // 配列の長さで割った余りを取ることで循環させる

        // 新しい銃をアクティブにする
        guns[currentGunIndex].SetActive(true);

        // 最後に武器を切り替えた時間を更新する
        lastSwitchTime = Time.time;

        // 武器アイコンの強調表示を更新する
        UpdateWeaponIcons();
    }

    // すべての銃を非アクティブにし、現在選択されている銃だけをアクティブにする
    void UpdateWeaponActiveState()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        guns[currentGunIndex].SetActive(true);
    }

    // 武器アイコンの強調表示を更新する
    void UpdateWeaponIcons()
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            if (i == currentGunIndex)
            {
                // 現在選択されている武器のアイコンを通常の色に設定
                weaponIcons[i].color = new Color(weaponIcons[i].color.r, weaponIcons[i].color.g, weaponIcons[i].color.b, 1f);
            }
            else
            {
                // 選択されていない武器のアイコンを薄暗くする
                weaponIcons[i].color = new Color(weaponIcons[i].color.r, weaponIcons[i].color.g, weaponIcons[i].color.b, 0.5f); // 透明度を0.5に設定
            }
        }
    }
}
