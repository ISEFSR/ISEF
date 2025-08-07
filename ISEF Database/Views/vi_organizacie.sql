CREATE VIEW [dbo].[vi_organizacie]
	AS SELECT		
					--co.Rok as Rok,

					sgm.Kod as SegmentKod,
					sgm.SkratenyText as SegmentShort,
					sgm.Popis as SegmentText,

					stp.Kod as StupenKod,
					stp.Nazov as StupenShort,
					stp.Popis as StupenText,

					po.Kod as PodriadenostKod,
					po.SkratenyNazov as PodriadenostSkrateny,
					po.Nazov as PodriadenostNazov,

					kr.Kod as KrajKod,
					kr.SkratenyNazov as  KrajShort,
					kr.Nazov as KrajNazov,

					ok.Kod as OkresKod,
					ok.SkratenyNazov as OkresShort,
					ok.Nazov as OkresNazov,

					ob.Kod as ObecKod,
					ob.Nazov as ObecNazov,

					co.Ico as OrgIco,
					co.Nazov as OrgNazov,
					co.Ulica as OrgUlica

	from			cis_org co 
	left join		cis_pod po on co.KodPodriadenost = po.Kod
	left join		cis_segment sgm on co.KodSegment = sgm.Kod
	left join		cis_stupen stp on co.KodStupen = stp.Kod
	left join		cis_obec ob on co.KodObec = ob.Kod
	left join		cis_okres ok on ob.KodOkres = ok.Kod
	left join		cis_kraj kr on ok.KodKraj = kr.Kod
