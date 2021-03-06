/*******************************************************************************
 * Copyright (c) 2016, George Sedov
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 ******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace KSPPreciseManeuver.UI {
[RequireComponent (typeof (RectTransform))]
public class TimeAlarmControl : MonoBehaviour {
  [SerializeField]
  private Text m_TimeValue = null;
  [SerializeField]
  private Toggle m_ToggleAlarm = null;

  private ITimeAlarmControl m_timeAlarmControl = null;

  public void SetTimeAlarmControl(ITimeAlarmControl timeAlarmControl) {
    m_timeAlarmControl = timeAlarmControl;
    updateTimeAlarm ();
    m_timeAlarmControl.registerUpdateAction (updateTimeAlarm);
  }

  public void OnDestroy () {
    m_timeAlarmControl.deregisterUpdateAction (updateTimeAlarm);
    m_timeAlarmControl = null;
  }

  public void ToggleAlarmAction (bool state) {
    m_timeAlarmControl.alarmToggle (state);
  }

  public void updateTimeAlarm () {
    m_TimeValue.text = m_timeAlarmControl.TimeValue;
    if (m_timeAlarmControl.AlarmAvailable) {
      m_ToggleAlarm.interactable = true;
      m_ToggleAlarm.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
      m_ToggleAlarm.isOn = m_timeAlarmControl.AlarmEnabled;
    } else {
      m_ToggleAlarm.interactable = false;
      m_ToggleAlarm.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, 0.25f);
      m_ToggleAlarm.isOn = false;
    }
  }
}
}
