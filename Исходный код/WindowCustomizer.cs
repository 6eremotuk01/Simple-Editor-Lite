using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TextEditor
{
    public static class WindowCustomizer
    {
        #region Sizeble
        public static readonly DependencyProperty Sizeble =
          DependencyProperty.RegisterAttached("Sizeble", typeof(bool), typeof(Window),
            new PropertyMetadata(true, new PropertyChangedCallback(OnSizebleChanged)));
        private static void OnSizebleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableSizeBox(window);
                    }
                    else
                    {
                        WindowHelper.DisableSizeBox(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetSizeble(DependencyObject d, bool value)
        {
            d.SetValue(Sizeble, value);
        }
        public static bool GetSizeble(DependencyObject d)
        {
            return (bool)d.GetValue(Sizeble);
        }
        #endregion

        #region CanClose
        public static readonly DependencyProperty CanClose =
          DependencyProperty.RegisterAttached("CanClose", typeof(bool), typeof(Window),
            new PropertyMetadata(true, new PropertyChangedCallback(OnCanCloseChanged)));
        private static void OnCanCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableClose(window);
                    }
                    else
                    {
                        WindowHelper.DisableClose(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetCanClose(DependencyObject d, bool value)
        {
            d.SetValue(CanClose, value);
        }
        public static bool GetCanClose(DependencyObject d)
        {
            return (bool)d.GetValue(CanClose);
        }
        #endregion

        #region CanMaximize
        public static readonly DependencyProperty CanMaximize =
          DependencyProperty.RegisterAttached("CanMaximize", typeof(bool), typeof(Window),
            new PropertyMetadata(true, new PropertyChangedCallback(OnCanMaximizeChanged)));
        private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableMaximize(window);
                    }
                    else
                    {
                        WindowHelper.DisableMaximize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetCanMaximize(DependencyObject d, bool value)
        {
            d.SetValue(CanMaximize, value);
        }
        public static bool GetCanMaximize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMaximize);
        }
        #endregion CanMaximize

        #region CanMinimize
        public static readonly DependencyProperty CanMinimize =
          DependencyProperty.RegisterAttached("CanMinimize", typeof(bool), typeof(Window),
            new PropertyMetadata(true, new PropertyChangedCallback(OnCanMinimizeChanged)));
        private static void OnCanMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableMinimize(window);
                    }
                    else
                    {
                        WindowHelper.DisableMinimize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetCanMinimize(DependencyObject d, bool value)

        {
            d.SetValue(CanMinimize, value);
        }
        public static bool GetCanMinimize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMinimize);
        }
        #endregion CanMinimize

        #region WindowHelper Nested Class

        public static class WindowHelper
        {
            private const Int32 GWL_STYLE = -16;
            private const Int32 WS_SYSMENU = 0x80000;
            private const Int32 WS_MAXIMIZEBOX = 0x00010000;
            private const Int32 WS_MINIMIZEBOX = 0x00020000;
            private const Int32 WS_SIZEBOX = 0x40000;

            [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
            private static extern Int32 GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);

            [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
            private static extern Int32 SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);

            /// <summary>
            /// Disables the close functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void DisableClose(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_SYSMENU);
                }
            }

            /// <summary>
            /// Disables the maximize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void DisableMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
                }
            }

            /// <summary>
            /// Disables the minimize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void DisableMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
                }
            }

            /// <summary>
            /// Disables the resize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void DisableSizeBox(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_SIZEBOX);
                }
            }

            /// <summary>
            /// Enables the close functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void EnableClose(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_SYSMENU);
                }
            }

            /// <summary>
            /// Enables the maximize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void EnableMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
                }
            }

            /// <summary>
            /// Enables the minimize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void EnableMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
                }
            }

            /// <summary>
            /// Enables the resize functionality of a WPF window.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void EnableSizeBox(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_SIZEBOX);
                }
            }


            /// <summary>
            /// Toggles the enabled state of a WPF window's close functionality.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void ToggleClose(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_SYSMENU) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_SYSMENU);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_SYSMENU);
                    }
                }
            }

            /// <summary>
            /// Toggles the enabled state of a WPF window's maximize functionality.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void ToggleMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_MAXIMIZEBOX) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
                    }
                }
            }

            /// <summary>
            /// Toggles the enabled state of a WPF window's minimize functionality.
            /// </summary>
            /// <param name="window">The WPF window to be modified.</param>
            public static void ToggleMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_MINIMIZEBOX) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
                    }
                }
            }

            public static void ToggleSizeBox(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_SIZEBOX) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_SIZEBOX);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_SIZEBOX);
                    }
                }
            }
        }
        #endregion WindowHelper Nested Class

        #region ShowIcon

        private const int GwlExstyle = -20;
        private const int SwpFramechanged = 0x0020;
        private const int SwpNomove = 0x0002;
        private const int SwpNosize = 0x0001;
        private const int SwpNozorder = 0x0004;
        private const int WsExDlgmodalframe = 0x0001;

        public static readonly DependencyProperty ShowIconProperty =
          DependencyProperty.RegisterAttached(
            "ShowIcon",
            typeof(bool),
            typeof(WindowCustomizer),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback((d, e) => RemoveIcon((Window)d))));


        public static Boolean GetShowIcon(UIElement element)
        {
            return (Boolean)element.GetValue(ShowIconProperty);
        }

        public static void RemoveIcon(Window window)
        {
            window.SourceInitialized += delegate {
                // Get this window's handle
                var hwnd = new WindowInteropHelper(window).Handle;

                // Change the extended window style to not show a window icon
                int extendedStyle = GetWindowLong(hwnd, GwlExstyle);
                SetWindowLong(hwnd, GwlExstyle, extendedStyle | WsExDlgmodalframe);

                // Update the window's non-client area to reflect the changes
                SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SwpNomove |
                  SwpNosize | SwpNozorder | SwpFramechanged);
            };
        }

        public static void SetShowIcon(UIElement element, Boolean value)
        {
            element.SetValue(ShowIconProperty, value);
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
          IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
          int x, int y, int width, int height, uint flags);
    }

    #endregion ShowIcon
}
