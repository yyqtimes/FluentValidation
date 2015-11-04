/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVFSystem"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MVFSystem.tools;

namespace MVFSystem.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StudentVM>();
            SimpleIoc.Default.Register<WPFValidatorVM>();
        }
        /// <summary>
        /// ���ַ�ʽ���Լ�,ʹ�÷�װ�ķ���AutofacInstaceFactory
        /// </summary>
        public MainViewModel StudentList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();

                //�򻯵ķ�����ʹ�ü򻯵ķ�������Ҫ�ڹ��캯����ע�� SimpleIoc.Default.Register<MainViewModel>();
                //return AutofacInstaceFactory.GetInstance<MainViewModel>();
            }
        }
        public StudentVM StudentForm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StudentVM>();
            }
        }
        public WPFValidatorVM WPFForm
        {
            get { return ServiceLocator.Current.GetInstance<WPFValidatorVM>(); }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}