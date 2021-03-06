﻿namespace NetworkVisualizer.Enums
{
    /// <summary>
    /// Representing all possible events.
    /// </summary>
    public enum Events
    {
        DEVICE_FOUND, DEVICE_DEFINED,
        FOCUS, UNFOCUS,
        DATA_ARRIVED, REQUEST_LOCAL_DATA, REQUEST_DATA,
        START_DEFINE, END_DEFINE,
        START_TEST, INFORM_START_TEST, SHOW_TEST, END_TEST,
        NEW_CONNECTION,
        SHOW_DEVICE_DATA, SHOW_CONNECTION_DATA,
        DATA_VISUALIZED,
        DRAW_CONNECTION, DRAW_CALL,
        CALL_FINISHED,
        HIGHLIGHT_OJECT, HIDE_OBJECT,
        OPEN_MENU, HIDE_MENU,
        DESTROY_VISUALIZATION
    }
}
