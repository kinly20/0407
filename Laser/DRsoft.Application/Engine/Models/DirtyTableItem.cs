namespace Engine.Models
{
    public class DirtyTableItem : NotifyBase
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

        private string? _machineId;
        public string? MachineId
        {
            get => _machineId;
            set
            {
                _machineId = value;
                NotifyOfPropertyChange(() => MachineId);
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

        private string? _isDirty;
        public string? IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                NotifyOfPropertyChange(() => IsDirty);
            }
        }

        private double? _dirtyX;
        public double? DirtyX
        {
            get => _dirtyX;
            set
            {
                _dirtyX = value;
                NotifyOfPropertyChange(() => DirtyX);
            }
        }

        private double? _dirtyY;
        public double? DirtyY
        {
            get => _dirtyY;
            set
            {
                _dirtyY = value;
                NotifyOfPropertyChange(() => DirtyY);
            }
        }

        private double? _dirtyWidth;
        public double? DirtyWidth
        {
            get => _dirtyWidth;
            set
            {
                _dirtyWidth = value;
                NotifyOfPropertyChange(() => DirtyWidth);
            }
        }

        private double? _dirtyHeight;
        public double? DirtyHeight
        {
            get => _dirtyHeight;
            set
            {
                _dirtyHeight = value;
                NotifyOfPropertyChange(() => DirtyHeight);
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
