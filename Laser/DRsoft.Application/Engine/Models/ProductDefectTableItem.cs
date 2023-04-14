namespace Engine.Models
{
    public class ProductDefectTableItem : NotifyBase
    {
        private string? _id;
        public string? Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private string? _groupId;
        public string? GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                NotifyOfPropertyChange(() => GroupId);
            }
        }

        private string? _silicaId;
        public string? SilicaId
        {
            get => _silicaId;
            set
            {
                _silicaId = value;
                NotifyOfPropertyChange(() => SilicaId);
            }
        }

        private int? _workStationId;
        public int? WorkStationId
        {
            get => _workStationId;
            set
            {
                _workStationId = value;
                NotifyOfPropertyChange(() => WorkStationId);
            }
        }

        private int? _laserId;
        public int? LaserId
        {
            get => _laserId;
            set
            {
                _laserId = value;
                NotifyOfPropertyChange(() => LaserId);
            }
        }

        private double? _padX;
        public double? PadX
        {
            get => _padX;
            set
            {
                _padX = value;
                NotifyOfPropertyChange(() => PadX);
            }
        }

        private double? _padY;
        public double? PadY
        {
            get => _padY;
            set
            {
                _padY = value;
                NotifyOfPropertyChange(() => PadY);
            }
        }

        private DateTime? _time;
        public DateTime? Time
        {
            get => _time;
            set
            {
                _time = value;
                NotifyOfPropertyChange(() => Time);
            }
        }
    }
}
